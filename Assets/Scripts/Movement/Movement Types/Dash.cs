using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private Vector2 currentMovement;
	private int currentDirection = 0;

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Dash (GameObject character) {
		this.character = character;
	}

	public int dashCheck(int movementSpeed) {
		float targetX = targetPoint.x;
		float targetY = targetPoint.y;
		float currentX = this.character.GetComponent<Rigidbody2D>().position.x;
		float currentY = this.character.GetComponent<Rigidbody2D>().position.y;
		float xVelocity = 0;
		float yVelocity = 0;
	
		if (currentX > targetX) {
			xVelocity -= movementSpeed;
			currentDirection = 2;
		} else {
			xVelocity += movementSpeed;
			currentDirection = 3;
		}

		if (currentY > targetY) {
			yVelocity -= movementSpeed;
			currentDirection = 0;
		} else {
			yVelocity += movementSpeed;
			currentDirection = 1;
		}
			
		currentMovement = new Vector2 (xVelocity, yVelocity);
		character.transform.position = Vector3.Lerp(character.transform.position, targetPoint, movementSpeed*Time.deltaTime);
		//character.GetComponent<Rigidbody2D>().velocity = currentMovement;
		return currentDirection;
	}

	public Vector2 TargetPoint {
		set {targetPoint = value;}
	}
}