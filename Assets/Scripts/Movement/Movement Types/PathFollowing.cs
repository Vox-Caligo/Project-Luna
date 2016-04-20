using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Goes on a path to given points and may loop that
 * path if so desired
 */
public class PathFollowing : BaseMovement
{
	// ways to follow the path
	private Pursuing pursuingFunctions;
	private Vector2 previousPoint;
	private ArrayList pathPoints;
	private int currentPoint = 0;
	private bool repeatable;

	// sets everything up for some path following
	public PathFollowing(GameObject character, ArrayList pathPoints, bool repeatable) : base(character) {
		this.pathPoints = pathPoints;
		pursuingFunctions = new Pursuing(character);
		this.character = character;
		this.repeatable = repeatable;
		previousPoint = new Vector2();
	}

	// moves between points, in order, and either stops or loops when reaching the end
	public int followPathPoints(float movementSpeed) {
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