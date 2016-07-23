using UnityEngine;
using System.Collections;

/**
 * AI specifically for movement that is the base for
 * all NPCs with specialized movement. Can also be used
 * alone for standard movements.
 */
public class DefaultMovementController : CharacterMovementController
{
	// variables that control the npc and its actions
	protected GameObject character;
	protected string characterName;
	protected string currentAction = "";
	protected float movementSpeed = 1;
	protected int currentDirection = 0;
	protected bool injureViaMovement = false;
	protected Vector2 targetPoint;
    protected int targetDirection;
    protected float followSpeed;
	protected Vector3 previousLocation;
    protected int teleportDirection;

	// classes that cause different movements
	protected Wandering wanderingFunctions;
	protected Pursuing pursuingFunctions;
	protected PathFollowing pathfollowingFunctions;
	protected ArrayList pathFollowingPoints;
	protected Bounce bouncingFunctions;
	protected NearbyTarget nearbyPlayerFunctions;
    protected Teleport teleportFunctions;

	// stores all needed movement functions and npc components
	public DefaultMovementController(string characterName, GameObject character) {
		this.characterName = characterName;
		this.character = character;
		wanderingFunctions = new Wandering(this.character, 1);
		pursuingFunctions = new Pursuing(this.character);
		pathfollowingFunctions = new PathFollowing(this.character, new ArrayList(){new Vector2(115f, 5f), new Vector2(115f, 10f), new Vector2(120f, 10f), new Vector2(120f, 5f)}, true);
		bouncingFunctions = new Bounce(this.character);
		nearbyPlayerFunctions = new NearbyTarget(this.character, targetPoint, .5f);
        teleportFunctions = new Teleport(this.character);
        previousLocation = character.transform.position;
	}
	
	// animation

	// what happens when given a current action
	public virtual void runScript() {	
		int moveCharacter = -1;

		switch(currentAction) {
	        case "halt":
		        character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
		        break;
	        case "wander":
		        moveCharacter = wanderingFunctions.wanderingCheck(movementSpeed);
		        break;
	        case "pursue":
		        moveCharacter = pursuingFunctions.pursuitCheck(targetPoint, movementSpeed);
		        break;
	        case "dash":
		        moveCharacter = pursuingFunctions.dashCheck(targetPoint, movementSpeed);
		        break;
	        case "flee":
		        moveCharacter = pursuingFunctions.fleeCheck (targetPoint, movementSpeed);
		        break;
	        case "path follow":
		        moveCharacter = pathfollowingFunctions.followPathPoints(movementSpeed);
		        break;
	        case "bounce":
		        moveCharacter = bouncingFunctions.inBounce(movementSpeed, 5, "north", "west");
		        break;
	        case "nearby-player":
		        moveCharacter = nearbyPlayerFunctions.nearbyPlayerCheck(targetPoint, movementSpeed);
		        break;
            case "random teleport":
                teleportFunctions.randomTeleport(3);
                break;
            case "target teleport":
                teleportFunctions.targetedTeleport(targetPoint, 3, teleportDirection);
                break;
            case "follow":
                moveCharacter = pursuingFunctions.followCharacter(targetPoint, targetDirection, followSpeed, movementSpeed);
                break;
        default:
			break;
		}

		// TODO: eventually just move this down here when wander is tweaked
		currentDirection = (moveCharacter != -1) ? moveCharacter : currentDirection;

    }

	// ways for the npc to respond to colliding with an object
	public void respondToCollision(Collision2D col) {
		if (col.gameObject.tag == "Structure") {
			// hitting a structure causes a ricochet (bounce) or stoppage (dash)
			if(currentAction == "bounce") {
				if (col.contacts [0].point.x != col.contacts [1].point.x) {
					bouncingFunctions.changeDirection (true);
				} else {
					bouncingFunctions.changeDirection (false);
				}
			} else if(currentAction == "dash") {
				pursuingFunctions.Dashing = false;
			}
		} else if(col.gameObject.tag == "Player") {
			// collisions with the player
			if(currentAction == "bounce") {
				// damages the player if they're hit
				injureViaMovement = true;
			} else if (currentAction == "dash") {
				// stops dashing and injures the player
				pursuingFunctions.Dashing = false;
				injureViaMovement = true;
				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			} else {
				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			}
		}
	}

	// get/set the npc current action
	public string CurrentAction {
		get {return currentAction;}
		set {currentAction = value;}
	}

	// get/set the npc current direction
	public int CurrentDirection {
		get {return currentDirection;}
		set {currentDirection = value;}
	}

	// get/set causes damage by collision
	public bool InjureViaMovement {
		get {return injureViaMovement;}
		set {injureViaMovement = value;}
	}

	// get/set the current npc point of interest
	public Vector2 TargetPoint {
		get {return targetPoint;}
		set {targetPoint = value;}
	}

    // get/set the targets current direction
    public int TargetDirection {
        get { return targetDirection; }
        set { targetDirection = value; }
    }

    // get/set the current npc point of interest
    public int TeleportDirection {
        set { teleportDirection = value; }
    }

    // set the current character follow speed
    public float FollowSpeed {
        set { followSpeed = value; }
    }
}