using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private Vector2 currentMovement;
	private bool dashing = false;

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}

	public void pursuitCheck(float movementSpeed) {
		if (!dashing) {
			float targetX = targetPoint.x;
			float targetY = targetPoint.y;
			float currentX = this.character.GetComponent<Rigidbody2D> ().position.x;
			float currentY = this.character.GetComponent<Rigidbody2D> ().position.y;
		}

		character.transform.position = Vector2.MoveTowards(character.transform.position, targetPoint, Time.deltaTime * movementSpeed);
	}

	public void dashCheck(float movementSpeed) {
		float newTargetPointX = (targetPoint.x - character.transform.position.x) * 2f;
		float newTargetPointY = (targetPoint.y - character.transform.position.y) * 2f;

		if (Mathf.Abs(newTargetPointX) <= 100 && Mathf.Abs(newTargetPointY) <= 100) {
			targetPoint.x += (targetPoint.x - character.transform.position.x) * 2f;
			targetPoint.y += (targetPoint.y - character.transform.position.y) * 2f;
		}

		dashing = true;
	}

	public void fleeCheck(float movementSpeed) {
		pursuitCheck (-movementSpeed);
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