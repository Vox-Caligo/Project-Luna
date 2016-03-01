using UnityEngine;
using System.Collections;

public class NearbyTarget : BaseMovement
{
	private float weaponRange;

	public NearbyTarget (GameObject character, Vector2 targetPoint, float weaponRange) : base(character) {
		this.weaponRange = weaponRange;
	}

	public int nearbyPlayerCheck(Vector2 targetPoint, float movementSpeed) {
		// use a compass rose and proceed to a point on the access near the player and attack
		// depending on the current direction, line them up

		Vector2 modifiedTargetPoint;
		int newDirection = -1;

		if(Mathf.Abs(character.transform.position.x - targetPoint.x) >= Mathf.Abs(character.transform.position.y - targetPoint.y)) {
			if(character.transform.position.x < targetPoint.x) {
				newDirection = 2;
				modifiedTargetPoint = new Vector2(targetPoint.x + weaponRange, targetPoint.y);
			} else {
				newDirection = 0;
				modifiedTargetPoint = new Vector2(targetPoint.x - weaponRange, targetPoint.y);
			}
		} else {
			if(character.transform.position.y < targetPoint.y) {
				newDirection = 1;
				modifiedTargetPoint = new Vector2(targetPoint.x, targetPoint.y - weaponRange);
			} else {
				newDirection = 3;
				modifiedTargetPoint = new Vector2(targetPoint.x, targetPoint.y + weaponRange);
			}
		}

		character.transform.position = Vector3.Lerp(character.transform.position, modifiedTargetPoint, movementSpeed*Time.deltaTime);
		return newDirection;
	}
}