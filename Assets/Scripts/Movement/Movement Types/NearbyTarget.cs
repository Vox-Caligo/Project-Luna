using UnityEngine;
using System.Collections;

public class NearbyTarget : MonoBehaviour
{
	private GameObject character;
	private Vector2 targetPoint;
	private int currentDirection;
	private float weaponRange;

	public NearbyTarget (GameObject character, Vector2 targetPoint, float weaponRange) {
		this.character = character;
		this.targetPoint = targetPoint;
		this.weaponRange = weaponRange;
	}

	public void nearbyPlayerCheck(int movementSpeed) {
		// use a compass rose and proceed to a point on the access near the player and attack
		// depending on the side then change the current direction
		//currentDirection = 0;
	}

	public Vector2 TargetPoint {
		set {targetPoint = value;}
	}

	public int CurrentDirection {
		get {return currentDirection;}
	}
}

