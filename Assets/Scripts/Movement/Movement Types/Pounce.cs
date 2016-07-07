using UnityEngine;
using System.Collections;

/**
 * Follows a target point (pursue), rushes without changing direction (dash),
 * or runs away (flee)
 */
public class Pounce : BaseMovement
{
	private Vector2 currentMovement;
	private Vector2 dashLocation;
	private bool dashing = false;
	
	public Pounce(GameObject character) : base(character) {}

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

	// calculates where the player will dash too until it stops
	private void dashLocationCalculator(Vector2 targetPoint) {
		float newTargetPointX = (targetPoint.x - character.transform.position.x) * 2f;
		float newTargetPointY = (targetPoint.y - character.transform.position.y) * 2f;

		if (Mathf.Abs(newTargetPointX) <= 100 && Mathf.Abs(newTargetPointY) <= 100) {
			dashLocation.x = targetPoint.x + (targetPoint.x - character.transform.position.x) * 2f;
			dashLocation.y = targetPoint.y + (targetPoint.y - character.transform.position.y) * 2f;
		}
	}

	// checks if the player is fleeing
	public int fleeCheck(Vector2 targetPoint, float movementSpeed) {
		return pursuitCheck (targetPoint, -movementSpeed);
	}

	// get/set if the character is dashing
	public bool Dashing {
		get { return dashing; }
		set { dashing = value; }
	}
}