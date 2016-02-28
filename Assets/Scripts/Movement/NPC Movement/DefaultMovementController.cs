using UnityEngine;
using System.Collections;

public class DefaultMovementController : CharacterMovementController
{
	protected GameObject character;
	protected string characterName;
	protected string currentAction = "";
	protected int movementSpeed = 1;
	protected int currentDirection = 0;
	protected bool injureViaMovement = false;
	protected Vector2 targetPoint;
	protected Vector3 previousLocation;
	
	protected float timerTick = 2f;
	protected float maxTimer = 2f;
	
	protected Wandering wanderingFunctions;
	protected Pursuing pursuingFunctions;
	protected PathFollowing pathfollowingFunctions;
	protected ArrayList pathFollowingPoints;
	protected Bounce bouncingFunctions;
	protected Dash dashingFunctions;
	protected NearbyTarget nearbyPlayerFunctions;
	
	public DefaultMovementController(string characterName, GameObject character) {
		this.characterName = characterName;
		this.character = character;
		wanderingFunctions = new Wandering(this.character);
		pursuingFunctions = new Pursuing(this.character);
		pathfollowingFunctions = new PathFollowing(this.character, new ArrayList(){new Vector2(115f, 5f), new Vector2(115f, 10f), new Vector2(120f, 10f), new Vector2(120f, 5f)}, true);
		bouncingFunctions = new Bounce(this.character);
		dashingFunctions = new Dash(this.character);
		nearbyPlayerFunctions = new NearbyTarget(this.character, targetPoint, .5f);
		previousLocation = character.transform.position;
	}
	
	// wandering around
	public virtual void wandering() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;						
		} else if (timerTick <= 0) {
			timerTick = Random.Range(maxTimer - maxTimer / .25f, maxTimer);
			wanderingFunctions.startWandering(currentDirection, movementSpeed);
		}
		currentDirection = wanderingFunctions.checkDistance(currentDirection);
	}
	
	// animation
	
	public virtual void runScript() {		
		calculateCurrentDirection();

		switch(currentAction) {
		case "halt":
			character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			break;
		case "wander":
			wandering();
			break;
		case "pursue":
			pursuingFunctions.pursuitCheck(movementSpeed);
			break;
		case "dash":
			pursuingFunctions.dashCheck(movementSpeed);
			break;
		case "flee":
			pursuingFunctions.fleeCheck (movementSpeed);
			break;
		case "path follow":
			pathfollowingFunctions.followPathPoints(movementSpeed);
			break;
		case "bounce":
			bouncingFunctions.inBounce(movementSpeed, 5, "north", "west");
			break;
		case "nearby-player":
			// face the player
			nearbyPlayerFunctions.nearbyPlayerCheck(movementSpeed, currentDirection);
			break;
		default:
			break;
		}
	}

	protected void calculateCurrentDirection() {
		Vector3 currentLocation = character.transform.position;

		if(currentLocation != previousLocation) {
			if((int)currentLocation.x != (int)previousLocation.x) {
				// check x coordinates
				if(currentLocation.x > previousLocation.x) {
					currentDirection = 1; // go right
				} else if(currentLocation.x < previousLocation.x) {
					currentDirection = 0; // go left
				}
			} else {
				// check y coordinates
				if((int)currentLocation.y > (int)previousLocation.y) {
					currentDirection = 2; // go up
				} else if((int)currentLocation.y < (int)previousLocation.y) {
					currentDirection = 3; // go down
				}
			}
		}

		previousLocation = currentLocation;
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