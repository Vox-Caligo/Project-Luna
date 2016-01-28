using UnityEngine;
using System.Collections;

public class DefaultMovement : MonoBehaviour
{
	protected GameObject character;

	protected int currentAction = 2;
	protected int movementSpeed = 0;
	protected int currentDirection = 0;
	
	protected float timerTick = 2f;
	protected float maxTimer = 2f;
	
	protected Wandering wanderingFunctions;
	protected Pursuing pursuingFunctions;
	protected PathFollowing pathfollowingFunctions;
	protected ArrayList pathFollowingPoints;
	
	public DefaultMovement(GameObject character) {
		this.character = character;
		wanderingFunctions = new Wandering(this.character);
		pursuingFunctions = new Pursuing(this.character);
		pathfollowingFunctions = new PathFollowing(this.character, new ArrayList(){new Vector2(115f, 5f), new Vector2(115f, 10f), new Vector2(120f, 10f), new Vector2(120f, 5f)}, true);
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
		case 0:
			wandering();
			break;
		case 1:
			currentDirection = pursuingFunctions.inPursuit();
			break;
		case 2:
			currentDirection = pathfollowingFunctions.followPathPoints();
			break;
		case 3:
			break;
		default:
			print("Not given a valid movement option");
			break;
		}
	}
	
	public int CurrentAction {
		get {return currentAction;}
		set {currentAction = value;}
	}
}

