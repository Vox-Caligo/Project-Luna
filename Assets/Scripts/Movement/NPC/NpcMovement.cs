using UnityEngine;
using System.Collections;

public class NpcMovement : MonoBehaviour {
	private int currentAction = 2;
	private int movementSpeed = 0;
	private int currentDirection = 0;
	
	private float timerTick = 2f;
	private float maxTimer = 2f;
	
	private Wandering wanderingFunctions;
	private Pursuing pursuingFunctions;
	private PathFollowing pathfollowingFunctions;
	
	// Use this for initialization
	void Start () {
		// testing purposes
		movementSpeed = 1;
		wanderingFunctions = new Wandering(this.gameObject);
		pursuingFunctions = new Pursuing(this.gameObject);
		pursuingFunctions.TargetPoint = GameObject.FindGameObjectWithTag("Player").rigidbody2D.position;
		
		ArrayList testPoints = new ArrayList();
		testPoints.Add(new Vector2(115f, 5f));
		testPoints.Add(new Vector2(115f, 10f));
		testPoints.Add(new Vector2(120f, 10f));
		testPoints.Add(new Vector2(120f, 5f));
		pathfollowingFunctions = new PathFollowing(this.gameObject, testPoints, true);
	}
	
	// wandering around
	private void wandering() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;						
		} else if (timerTick <= 0) {
			timerTick = Random.Range(maxTimer - maxTimer / .25f, maxTimer);
			currentDirection = wanderingFunctions.startWandering(currentDirection, movementSpeed);
		}
		currentDirection = wanderingFunctions.checkDistance(currentDirection);
	}
	
	// searching around
	private void searching() { }
	
	// animation
	
	// Update is called once per frame
	void FixedUpdate () {
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
}
