using UnityEngine;
using System.Collections;

public class BaseMovement : MonoBehaviour
{
	protected GameObject character;

	public BaseMovement(GameObject character) {
		this.character = character;
	}

	protected int calculateDirection(Vector2 currentPosition, Vector2 newPosition) {
		if((int)currentPosition.x != (int)newPosition.x) {
			// check x coordinates
			if(currentPosition.x < newPosition.x) {
				return 2; // go right
			} else {
				return 0; // go left
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