using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private Vector2 currentMovement;
	private int currentDirection = 0;

	// dashing variables
	private bool dashing = false; 

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}

	public int pursuitCheck(int movementSpeed, bool dashing) {
		if (!this.dashing) {
			float targetX = targetPoint.x;
			float targetY = targetPoint.y;
			float currentX = this.character.GetComponent<Rigidbody2D> ().position.x;
			float currentY = this.character.GetComponent<Rigidbody2D> ().position.y;

			if (dashing) {
				this.dashing = dashing;
				targetPoint.x += (targetPoint.x - character.transform.position.x) * 1.2f;
				targetPoint.y += (targetPoint.y - character.transform.position.y) * 1.2f;
			}

			if (currentX > targetX) 
				currentDirection = 2;
			else
				currentDirection = 3;

			if (currentY > targetY)
				currentDirection = 0;
			else
				currentDirection = 1;
		}

		if (this.dashing)
			movementSpeed *= 5;
		
		character.transform.position = Vector2.MoveTowards(character.transform.position, targetPoint, Time.deltaTime * movementSpeed);
		return currentDirection;
	}

	public Vector2 TargetPoint {
		set { targetPoint = value; }
	}

	public bool Dashing {
		get { return dashing; }
		set { dashing = value; }
	}
}