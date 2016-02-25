using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{
	private GameObject character;
	private bool currentlyRolling = false;
	private string verticalDirection;
	private string horizontalDirection;

	public Bounce (GameObject character) {
		this.character = character;
	}

	public void inBounce(float movementSpeed, float duration, string verticalDirection, string horizontalDirection) {
		float xVelocity = 0;
		float yVelocity = 0;

		if(!currentlyRolling) {
			this.verticalDirection = verticalDirection;
			this.horizontalDirection = horizontalDirection;
			currentlyRolling = true;
		}

		if(this.verticalDirection == "north")
			yVelocity -= movementSpeed;
		else
			yVelocity += movementSpeed;

		if(this.horizontalDirection == "east")
			xVelocity -= movementSpeed;
		else
			xVelocity += movementSpeed;

		character.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity,yVelocity);
	}

	public void changeDirection(bool verticalCollision) {
		if(verticalCollision) {
			if(verticalDirection == "north")
				verticalDirection = "south";
			else
				verticalDirection = "north";
		} else {
			if(horizontalDirection == "east")
				horizontalDirection = "west";
			else
				horizontalDirection = "east";
		}
	}
}