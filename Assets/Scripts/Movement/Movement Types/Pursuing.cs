using UnityEngine;
using System.Collections;

/**
 * Follows a target point (pursue), rushes without changing direction (dash),
 * or runs away (flee)
 */
public class Pursuing : BaseMovement
{
	private Vector2 currentMovement;
	private Vector2 dashLocation;
	private bool dashing = false;
	
	public Pursuing (GameObject character) : base(character) {}

	// checks if it should head to a position or no longer pursuing if there
	public int pursuitCheck(Vector2 targetPoint, float movementSpeed) {
		float targetX = targetPoint.x;
		float targetY = targetPoint.y;
		float currentX = this.character.GetComponent<Rigidbody2D> ().position.x;
		float currentY = this.character.GetComponent<Rigidbody2D> ().position.y;

		Vector2 currentPosition = character.transform.position;
		character.transform.position = Vector2.MoveTowards(character.transform.position, targetPoint, Time.deltaTime * movementSpeed);
		Vector2 newPosition = character.transform.position;
		return calculateDirection(currentPosition, newPosition);
	}

	// checks if the character is dashing and pursues if it's supposed to
	public int dashCheck(Vector2 targetPoint, float movementSpeed) {
		if (!dashing) {
			dashing = true;
			dashLocationCalculator(targetPoint);
		} else {
			dashLocationCalculator(dashLocation);
		}

		return pursuitCheck(dashLocation, movementSpeed * 4);
	}

    // calculates where the character will dash too until it stops
    private void dashLocationCalculator(Vector2 targetPoint) {
		float newTargetPointX = (targetPoint.x - character.transform.position.x) * 2f;
		float newTargetPointY = (targetPoint.y - character.transform.position.y) * 2f;

		if (Mathf.Abs(newTargetPointX) <= 100 && Mathf.Abs(newTargetPointY) <= 100) {
			dashLocation.x = targetPoint.x + (targetPoint.x - character.transform.position.x) * 2f;
			dashLocation.y = targetPoint.y + (targetPoint.y - character.transform.position.y) * 2f;
		}
	}

	// checks if the character is fleeing
	public int fleeCheck(Vector2 targetPoint, float movementSpeed) {
		return pursuitCheck (targetPoint, -movementSpeed);
	}

    // have the character follow a character
    public int followCharacter(Vector2 targetPoint, int leaderDirection, float followDistance, float movementSpeed) {
        // checks if the character is within a certain distance

        if (Vector2.Distance(character.transform.position, targetPoint) > followDistance) {
            Vector2 pointToApproach;
            if (leaderDirection == 0) {
                pointToApproach = new Vector3(targetPoint.x + followDistance, targetPoint.y);
            } else if (leaderDirection == 1) {
                pointToApproach = new Vector3(targetPoint.x, targetPoint.y - followDistance);
            } else if (leaderDirection == 2) {
                pointToApproach = new Vector3(targetPoint.x - followDistance, targetPoint.y);
            } else {
                pointToApproach = new Vector3(targetPoint.x, targetPoint.y + followDistance);
            }

            return pursuitCheck(pointToApproach, movementSpeed);
        }

        return -1;
    }

    // get/set if the character is dashing
    public bool Dashing {
		get { return dashing; }
		set { dashing = value; }
	}
}