﻿using UnityEngine;
using System.Collections;

/**
 * Character rolls around the stage and bounces off certain structures
 * and/or the player
 */
public class Bounce : BaseMovement
{
	private bool currentlyRolling = false;
	private string verticalDirection;
	private string horizontalDirection;

	public Bounce (GameObject character) : base(character) {}

	// moves in directions based upon straight lines and speeds. Continues to move
	// and just bounce off of objects
	public int inBounce(float movementSpeed, float duration, string verticalDirection, string horizontalDirection) {
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

		Vector2 currentPosition = character.transform.position;
		character.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity,yVelocity);
		Vector2 newPosition = character.transform.position;

		return calculateDirection(currentPosition, newPosition);
	}

	// reflect to a different direction after a collision
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