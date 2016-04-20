using UnityEngine;
using System.Collections;

/**
 * Randomly wanders around the stage
 */
public class Wandering : BaseMovement
{
	// gets the current direction and start point
	private Vector2 currentDirectionVelocity = new Vector2();
	private Vector2 startPoint;

	// timer variables
	private float timerTick = 2f;
	private float maxTimer = 2f;

	// sets the point where it starts
	public Wandering (GameObject character) : base(character) {
		startPoint = character.GetComponent<Rigidbody2D>().position;
	}

	// wandering around
	public int wanderingCheck(float movementSpeed) {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return proceedToWandering(movementSpeed, false);						
		} else {
			timerTick = Random.Range(maxTimer - maxTimer / .25f, maxTimer);
			return proceedToWandering(movementSpeed, true);
		}
	}

	// has the player move and sets the direction they are going
	private int proceedToWandering(float movementSpeed, bool determineNewDirection) {
		int newCurrentDirection = -1;

		if(determineNewDirection) {
			int newDirection = Random.Range(0, 20);

			if(newDirection <= 5) {
				currentDirectionVelocity = new Vector2(-movementSpeed, 0);
				newCurrentDirection = 0;
			} else if(newDirection <= 10) {
				currentDirectionVelocity = new Vector2(0, movementSpeed);
				newCurrentDirection = 1;
			} else if(newDirection <= 15) {
				currentDirectionVelocity = new Vector2(movementSpeed, 0);
				newCurrentDirection = 2;
			} else if(newDirection <= 20) {
				currentDirectionVelocity = new Vector2(0, -movementSpeed);
				return 3;
			} else {
				currentDirectionVelocity = new Vector2(0,0);
				newCurrentDirection = -1;
			}
		}

		character.GetComponent<Rigidbody2D>().velocity = currentDirectionVelocity;
		return newCurrentDirection;
	}
}