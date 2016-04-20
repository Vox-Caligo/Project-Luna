using UnityEngine;
using System.Collections;

/**
 * Looks towards the player
 */
public class FacePlayer : BaseMovement
{
	private Vector2 targetPoint;

	public FacePlayer (GameObject character) : base(character) {}

	public void facePlayerCheck(GameObject targetPoint) {

	}
}