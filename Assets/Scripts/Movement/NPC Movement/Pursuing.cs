using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private Vector2 currentMovement;
	private bool dashing = false;
	private int currentDirection = 0;

	// timer properties
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}

	public int inPursuit(int movementSpeed, bool dashing = false) {
		if (!this.dashing)
			pursuitCheck (movementSpeed, dashing);
		else
			dashCheck ();
		
		return currentDirection;
	}

	private int pursuitCheck(int movementSpeed, bool dashing) {
		float targetX = targetPoint.x;
		float targetY = targetPoint.y;
		float currentX = this.character.GetComponent<Rigidbody2D>().position.x;
		float currentY = this.character.GetComponent<Rigidbody2D>().position.y;
		float xVelocity = 0;
		float yVelocity = 0;
	
		// float total distance - needed for far range to run at the player accurately
		float xDistance = Vector2.Distance(new Vector2(currentX, 0), new Vector2(targetX, 0));
		float yDistance = Vector2.Distance(new Vector2(0, currentY), new Vector2(0, targetY));

		if (xDistance > character.GetComponent<BoxCollider2D> ().size.x * 3) {
			if (currentX > targetX) {
				xVelocity -= movementSpeed;
				currentDirection = 2;
			} else {
				xVelocity += movementSpeed;
				currentDirection = 3;
			}
		}

		if (yDistance > character.GetComponent<BoxCollider2D> ().size.y * 2.5f) {
			if (currentY > targetY) {
				yVelocity -= movementSpeed;
				currentDirection = 0;
			} else {
				yVelocity += movementSpeed;
				currentDirection = 1;
			}
		}

		if (dashing) {
			this.dashing = dashing;
			xVelocity *= 5f;
			yVelocity *= 5f;
		}
			
		currentMovement = new Vector2 (xVelocity, yVelocity);
			
		character.GetComponent<Rigidbody2D>().velocity = currentMovement;
		return currentDirection;
	}

	private int dashCheck() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			character.GetComponent<Rigidbody2D>().velocity = currentMovement;
		} else if (timerTick <= 0) {
			dashing = false;
			timerTick = maxTimer;
		}
		return currentDirection;
	}

	public Vector2 TargetPoint {
		set {targetPoint = value;}
	}
}