using UnityEngine;
using System.Collections;

public class Pursuing : BaseMovement
{
	private Vector2 currentMovement;
	private Vector2 dashLocation;
	private bool dashing = false;

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Pursuing (GameObject character) : base(character) {}

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

	public int dashCheck(Vector2 targetPoint, float movementSpeed) {
		if (!dashing) {
			dashing = true;
			dashLocationCalculator(targetPoint);
		} else {
			dashLocationCalculator(dashLocation);
		}

		return pursuitCheck(dashLocation, movementSpeed * 4);
	}

	private void dashLocationCalculator(Vector2 targetPoint) {
		float newTargetPointX = (targetPoint.x - character.transform.position.x) * 2f;
		float newTargetPointY = (targetPoint.y - character.transform.position.y) * 2f;

		if (Mathf.Abs(newTargetPointX) <= 100 && Mathf.Abs(newTargetPointY) <= 100) {
			dashLocation.x = targetPoint.x + (targetPoint.x - character.transform.position.x) * 2f;
			dashLocation.y = targetPoint.y + (targetPoint.y - character.transform.position.y) * 2f;
		}
	}

	public int fleeCheck(Vector2 targetPoint, float movementSpeed) {
		return pursuitCheck (targetPoint, -movementSpeed);
	}

	public bool Dashing {
		get { return dashing; }
		set { dashing = value; }
	}
}