using UnityEngine;
using System.Collections;

public class NpcMovement : MonoBehaviour {
	private int currentAction = 1;
	private int movementSpeed = 0;
	private int currentDirection = 0;
	
	private float timerTick = 2f;
	private float maxTimer = 2f;
	
	private Wandering wanderingFunctions;
	private Pursuing pursuingFunctions;
	
	// Use this for initialization
	void Start () {
		// testing purposes
		movementSpeed = 1;
		wanderingFunctions = new Wandering(this.gameObject);
		pursuingFunctions = new Pursuing(this.gameObject);
		pursuingFunctions.TargetCharacter = GameObject.FindGameObjectWithTag("Player");
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
	
	// following a path
	private void pathFinding() {
	
	}
	
	// searching around
	private void searching() {
	
	}
	
	public void startPursuit(GameObject targetCharacter) {
		pursuingFunctions.TargetCharacter = targetCharacter;
	}
	
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
			break;
		case 3:
			break;
		default:
			print("Not given a valid movement option");
			break;
		}
	}
}
