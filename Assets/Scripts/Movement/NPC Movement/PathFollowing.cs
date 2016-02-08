using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowing : MonoBehaviour
{
	private Pursuing pursuingFunctions;
	private Vector2 previousPoint;
	private GameObject character;
	private ArrayList pathPoints;
	private int currentPoint = 0;
	private bool repeatable;
	
	public PathFollowing(GameObject character, ArrayList pathPoints, bool repeatable) {
		this.pathPoints = pathPoints;
		pursuingFunctions = new Pursuing(character);
		this.character = character;
		this.repeatable = repeatable;
		previousPoint = new Vector2();
	}

	public int followPathPoints(int movementSpeed) {
		if(previousPoint != character.GetComponent<Rigidbody2D>().position) {
			previousPoint = character.GetComponent<Rigidbody2D>().position;
			pursuingFunctions.TargetPoint = (Vector2)pathPoints[currentPoint];
		} else {
			if(currentPoint < pathPoints.Count - 1) {
				currentPoint++;
				previousPoint = new Vector2();
			} else if(repeatable) {
				currentPoint = 0;
				previousPoint = new Vector2();
			}
		}
		
		return pursuingFunctions.inPursuit(movementSpeed);
	}
}