using UnityEngine;
using System.Collections;

public class NearbyTarget : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private float weaponRange;

	public NearbyTarget (GameObject character, Vector2 targetPoint, float weaponRange) {
		this.character = character;
		this.targetPoint = targetPoint;
		this.weaponRange = weaponRange;
	}

	public void nearbyPlayerCheck(int movementSpeed, int currentDirection) {
		// use a compass rose and proceed to a point on the access near the player and attack
		// depending on the current direction, line them up

		Vector2 modifiedTargetPoint;

		print("Checking the currentDirection: " + currentDirection);
		switch(currentDirection) {
		case 0:	// left
			modifiedTargetPoint = new Vector2(targetPoint.x - weaponRange, targetPoint.y);
			break;
		case 1:	// right
			modifiedTargetPoint = new Vector2(targetPoint.x + weaponRange, targetPoint.y);
			break;
		case 2:	// up
			modifiedTargetPoint = new Vector2(targetPoint.x, targetPoint.y - weaponRange);
			break;
		case 3:	// down
			modifiedTargetPoint = new Vector2(targetPoint.x, targetPoint.y + weaponRange);
			break;
		default:
			print("Not a valid direction");
			modifiedTargetPoint = new Vector2();
			break;
		}

		if(currentDirection == 0) {

		}

		character.transform.position = Vector3.Lerp(character.transform.position, modifiedTargetPoint, movementSpeed*Time.deltaTime);
	}

	public Vector2 TargetPoint {
		set {targetPoint = value;}
	}
}