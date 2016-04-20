using UnityEngine;
using System.Collections;

/**
 * A basic set of movements for a character to go
 */
public class BaseMovement : MonoBehaviour
{
	protected GameObject character;

	// sets which character is being moved
	public BaseMovement(GameObject character) {
		this.character = character;
	}

	// figures out which direction the character is going
	protected int calculateDirection(Vector2 currentPosition, Vector2 newPosition) {
		if((int)currentPosition.x != (int)newPosition.x) {
			// check x coordinates
			if(currentPosition.x < newPosition.x) {
				return 0; // go left
			} else {
				return 2; // go right
			}
		} else if((int)currentPosition.y != (int)newPosition.y) {
			// check y coordinates
			if((int)currentPosition.y < (int)newPosition.y) {
				return 1; // go up
			} else {
				return 3; // go down
			}
		}

		return -1;
	}
}