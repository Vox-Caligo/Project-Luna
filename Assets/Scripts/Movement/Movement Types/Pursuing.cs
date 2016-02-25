using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private Vector2 currentMovement;
	private int currentDirection = 0;
	private bool dashing = false;

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}

	public int pursuitCheck(float movementSpeed) {
		if (!dashing) {
			float targetX = targetPoint.x;
			float targetY = targetPoint.y;
			float currentX = this.character.GetComponent<Rigidbody2D> ().position.x;
			float currentY = this.character.GetComponent<Rigidbody2D> ().position.y;

			if (currentX > targetX) 
				currentDirection = 2;
			else
				currentDirection = 3;

			if (currentY > targetY)
				currentDirection = 0;
			else
				currentDirection = 1;
		}

		character.transform.position = Vector2.MoveTowards(character.transform.position, targetPoint, Time.deltaTime * movementSpeed);
		return currentDirection;
	}

	public int dashCheck(float movementSpeed) {
		float newTargetPointX = (targetPoint.x - character.transform.position.x) * 2f;
		float newTargetPointY = (targetPoint.y - character.transform.position.y) * 2f;

		if (Mathf.Abs(newTargetPointX) <= 100 && Mathf.Abs(newTargetPointY) <= 100) {
			targetPoint.x += (targetPoint.x - character.transform.position.x) * 2f;
			targetPoint.y += (targetPoint.y - character.transform.position.y) * 2f;
		}

		int newDirection = pursuitCheck (movementSpeed * 2);
		dashing = true;
		return newDirection;
	}

	public int fleeCheck(float movementSpeed) {
		return pursuitCheck (-movementSpeed);
	}

	public Vector2 TargetPoint {
		get { return targetPoint; }
		set { targetPoint = value; }
	}

	public bool Dashing {
		get { return dashing; }
		set { dashing = value; }
	}
}