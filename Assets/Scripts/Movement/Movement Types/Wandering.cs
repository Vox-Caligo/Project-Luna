using UnityEngine;
using System.Collections;

/**
 * Randomly wanders around the stage
 */
public class Wandering : BaseMovement
{
    // gets the current direction and start point
    private int currentDirection = -1;
	private Vector2 currentDirectionVelocity = new Vector2();
	private Vector2 startPoint;
    private float allowableDistance;

    // timer variables
    private float maxWanderingDelay = 5;
	private UtilTimer wanderingMotionTimer;

	// sets the point where it starts
	public Wandering (GameObject character, float allowableDistance) : base(character) {
		startPoint = character.GetComponent<Rigidbody2D>().position;
        wanderingMotionTimer = new UtilTimer(maxWanderingDelay, maxWanderingDelay);
        this.allowableDistance = allowableDistance;
    }

	// wandering around
	public int wanderingCheck(float movementSpeed) {
        if (wanderingMotionTimer.runningTimerCountdown() && Vector2.Distance(startPoint, character.transform.position) < allowableDistance) {
            return proceedToWandering(movementSpeed, false);						
		} else {
			wanderingMotionTimer.RunningTimerMax = Random.Range(maxWanderingDelay - maxWanderingDelay / .25f, maxWanderingDelay);
			return proceedToWandering(movementSpeed, true);
		}
	}

	// has the player move and sets the direction they are going
	private int proceedToWandering(float movementSpeed, bool determineNewDirection) {
		int newCurrentDirection = -1;
        Vector2 characterPosition = character.transform.position;

        if (determineNewDirection) {
            newCurrentDirection = Random.Range(0, 50);
        }

        // check that either the direction is -1 or that it is not the exact opposite and it's within the distance
        do {
            if (newCurrentDirection <= 5 && currentDirection != 2) {
                currentDirectionVelocity = new Vector2(characterPosition.x - movementSpeed, 0);
                currentDirection = 0;
            } else if (newCurrentDirection <= 10 && currentDirection != 3) {
                currentDirectionVelocity = new Vector2(0, characterPosition.y + movementSpeed);
                currentDirection = 1;
            } else if (newCurrentDirection <= 15 && currentDirection != 0) {
                currentDirectionVelocity = new Vector2(characterPosition.x + movementSpeed, 0);
                currentDirection = 2;
            } else if (newCurrentDirection <= 20 && currentDirection != 1) {
                currentDirectionVelocity = new Vector2(0, characterPosition.y - movementSpeed);
                return 3;
            } else if(newCurrentDirection > 20) {
                currentDirectionVelocity = new Vector2(0, 0);
                currentDirection = -1;
            }
        } while (Vector2.Distance(startPoint, currentDirectionVelocity) > allowableDistance && newCurrentDirection != -1);
        // causing an infinite loop

        character.transform.position = Vector2.MoveTowards(character.transform.position, currentDirectionVelocity, Time.deltaTime * movementSpeed);
        return newCurrentDirection;
	}
}