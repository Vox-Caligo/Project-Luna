using UnityEngine;
using System.Collections;

public class DefaultMovementController : CharacterMovementController
{
	protected GameObject character;
	protected string characterName;
	protected string currentAction = "wander";
	protected int movementSpeed = 1;
	protected int currentDirection = 0;
	protected bool injureViaMovement = false;
	
	protected float timerTick = 2f;
	protected float maxTimer = 2f;
	
	protected Wandering wanderingFunctions;
	protected Pursuing pursuingFunctions;
	protected PathFollowing pathfollowingFunctions;
	protected ArrayList pathFollowingPoints;
	protected Bounce bouncingFunctions;
	protected Dash dashingFunctions;
	
	public DefaultMovementController(string characterName, GameObject character) {
		this.characterName = characterName;
		this.character = character;
		wanderingFunctions = new Wandering(this.character);
		pursuingFunctions = new Pursuing(this.character);
		pathfollowingFunctions = new PathFollowing(this.character, new ArrayList(){new Vector2(115f, 5f), new Vector2(115f, 10f), new Vector2(120f, 10f), new Vector2(120f, 5f)}, true);
		bouncingFunctions = new Bounce(this.character);
		dashingFunctions = new Dash(this.character);
	}
	
	// wandering around
	public virtual void wandering() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;						
		} else if (timerTick <= 0) {
			timerTick = Random.Range(maxTimer - maxTimer / .25f, maxTimer);
			currentDirection = wanderingFunctions.startWandering(currentDirection, movementSpeed);
		}
		currentDirection = wanderingFunctions.checkDistance(currentDirection);
	}
	
	// animation
	
	public virtual void runScript() {		
		switch(currentAction) {
		case "halt":
			character.GetComponent<Rigidbody2D> ().velocity = new Vector2 ();
			break;
		case "wander":
			wandering();
			break;
		case "pursue":
			currentDirection = pursuingFunctions.pursuitCheck(movementSpeed);
			break;
		case "dash":
			currentDirection = pursuingFunctions.dashCheck(movementSpeed);
			break;
		case "flee":
			currentDirection = pursuingFunctions.fleeCheck (movementSpeed);
			break;
		case "path follow":
			currentDirection = pathfollowingFunctions.followPathPoints(movementSpeed);
			break;
		case "bounce":
			bouncingFunctions.inBounce(movementSpeed, 5, "north", "west");
			break;
		default:
			print("Not given a valid movement option");
			break;
		}
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

	public bool InjureViaMovement {
		get {return injureViaMovement;}
		set {injureViaMovement = value;}
	}
}