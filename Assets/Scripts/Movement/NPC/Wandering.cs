using UnityEngine;
using System.Collections;

public class Wandering : MonoBehaviour
{
	private GameObject character;
	private Vector2 startPoint;
	
	public Wandering (GameObject character) {
		this.character = character;
		startPoint = character.rigidbody2D.position;
	}
	
	public int startWandering(int currentDirection, int movementSpeed) {
		if(Random.Range(0, 100) <= 20) {
			int newDirection = Random.Range(0, 100) % 2;
			
			if(newDirection <= 20) {
				if(currentDirection % 2 == 0) {
					if(newDirection == 0) {
						currentDirection = 1;
					} else {
						currentDirection = 3;
						movementSpeed *= -1;
					}
					character.rigidbody2D.velocity = new Vector2(movementSpeed,0);
				} else {
					if(newDirection == 0) {
						currentDirection = 0;
					} else {
						currentDirection = 2;
						movementSpeed *= -1;
					}
					character.rigidbody2D.velocity = new Vector2(0,movementSpeed);
				}
			} else {
				character.rigidbody2D.velocity = new Vector2(0,0);
			}
		}
		return currentDirection;
	}
	
	// Update is called once per frame
	public int checkDistance(int currentDirection) {
		if(Vector2.Distance(startPoint, this.character.rigidbody2D.position) > 2) {
			character.rigidbody2D.velocity = new Vector2(character.rigidbody2D.velocity.x * -1, character.rigidbody2D.velocity.y * -1);
			
			if(currentDirection < 2)
				currentDirection += 2;
			else
				currentDirection -= 2;
		}
		return currentDirection;
	}
}