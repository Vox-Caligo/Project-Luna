using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private GameObject targetCharacter;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}

	public int inPursuit() {
		float targetX = targetCharacter.rigidbody2D.position.x;
		float targetY = targetCharacter.rigidbody2D.position.y;
		float currentX = this.character.rigidbody2D.position.x;
		float currentY = this.character.rigidbody2D.position.y;
		float xVelocity = 0;
		float yVelocity = 0;
	
		float xDistance = Vector2.Distance(new Vector2(currentX, 0), new Vector2(targetX, 0));
		float yDistance = Vector2.Distance(new Vector2(0, currentY), new Vector2(0, targetY));
		int currentDirection = 0;
		
		if(xDistance > character.GetComponent<BoxCollider2D>().size.x * 3) {
			if(currentX > targetX) {
				xVelocity -= 1;
				currentDirection = 2;
			} else {
				xVelocity += 1;
				currentDirection = 3;
			}
		}

		if(yDistance > character.GetComponent<BoxCollider2D>().size.y * 2.5f) {
			if(currentY > targetY) {
				yVelocity -= 1;
				currentDirection = 0;
			} else {
				yVelocity += 1;
				currentDirection = 1;
			}
		}
		
		character.rigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
		return currentDirection;
	}
	
	public GameObject TargetCharacter {
		set {targetCharacter = value;}
	}
}