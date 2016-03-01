using UnityEngine;
using System.Collections;

public class DefaultMovementController : CharacterMovementController
{
	protected GameObject character;
	protected string characterName;
	protected string currentAction = "";
	protected float movementSpeed = 1;
	protected int currentDirection = 0;
	protected bool injureViaMovement = false;
	protected Vector2 targetPoint;
	protected Vector3 previousLocation;
	
	protected Wandering wanderingFunctions;
	protected Pursuing pursuingFunctions;
	protected PathFollowing pathfollowingFunctions;
	protected ArrayList pathFollowingPoints;
	protected Bounce bouncingFunctions;
	protected NearbyTarget nearbyPlayerFunctions;
	
	public DefaultMovementController(string characterName, GameObject character) {
		this.characterName = characterName;
		this.character = character;
		wanderingFunctions = new Wandering(this.character);
		pursuingFunctions = new Pursuing(this.character);
		pathfollowingFunctions = new PathFollowing(this.character, new ArrayList(){new Vector2(115f, 5f), new Vector2(115f, 10f), new Vector2(120f, 10f), new Vector2(120f, 5f)}, true);
		bouncingFunctions = new Bounce(this.character);
		nearbyPlayerFunctions = new NearbyTarget(this.character, targetPoint, .5f);
		previousLocation = character.transform.position;
	}
	
	// animation
	
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
		default:
			break;
		}

		// TODO: eventually just move this down here when wander is tweaked
		currentDirection = (moveCharacter != -1) ? moveCharacter : currentDirection;
	}

	public void respondToCollision(Collision2D col) {
		if (col.gameObject.tag == "Structure") {
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
			if(currentAction == "bounce") {
				injureViaMovement = true;
			} else if (currentAction == "dash") {
				pursuingFunctions.Dashing = false;
				injureViaMovement = true;
				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			} else {
				print("testing the stop");
				character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			}
		}
	}
	
	public string CurrentAction {
		get {return currentAction;}
		set {currentAction = value;}
	}

	public int CurrentDirection {
		get {return currentDirection;}
		set {currentDirection = value;}
	}

	public bool InjureViaMovement {
		get {return injureViaMovement;}
		set {injureViaMovement = value;}
	}

	public Vector2 TargetPoint {
		get {return targetPoint;}
		set {targetPoint = value;}
	}
}