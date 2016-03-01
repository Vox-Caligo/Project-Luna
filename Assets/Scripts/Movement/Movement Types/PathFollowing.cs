using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFollowing : BaseMovement
{
	private Pursuing pursuingFunctions;
	private Vector2 previousPoint;
	private ArrayList pathPoints;
	private int currentPoint = 0;
	private bool repeatable;
	
	public PathFollowing(GameObject character, ArrayList pathPoints, bool repeatable) : base(character) {
		this.pathPoints = pathPoints;
		pursuingFunctions = new Pursuing(character);
		this.character = character;
		this.repeatable = repeatable;
		previousPoint = new Vector2();
	}

	public int followPathPoints(int movementSpeed) {
		if(previousPoint != character.GetComponent<Rigidbody2D>().position) {
			previousPoint = character.GetComponent<Rigidbody2D>().position;
			return pursuingFunctions.pursuitCheck((Vector2)pathPoints[currentPoint], movementSpeed);
		} else {
			if(currentPoint < pathPoints.Count - 1) {
				currentPoint++;
				previousPoint = new Vector2();
			} else if(repeatable) {
				currentPoint = 0;
				previousPoint = new Vector2();
			}
		}

		return -1;
	}
}