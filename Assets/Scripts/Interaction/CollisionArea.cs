using UnityEngine;
using System.Collections;

public class CollisionArea : MonoBehaviour
{
	protected GameObject collisionDetector;
	protected BoxCollider2D collisionDetectionBox;
	protected float boxWidth;
	protected float boxHeight;

	public CollisionArea(GameObject parentCharacter, float boxWidth, float boxHeight) {
		collisionDetector = new GameObject();
		collisionDetector.transform.parent = parentCharacter.transform;
		collisionDetector.transform.position = collisionDetector.transform.parent.position;
		collisionDetector.AddComponent<InteractableItem>();

		collisionDetectionBox = collisionDetector.AddComponent<BoxCollider2D>();
		collisionDetectionBox.isTrigger = true;
		boxWidth = parentCharacter.GetComponent<BoxCollider2D> ().bounds.extents.x * 2;
		boxHeight = parentCharacter.GetComponent<BoxCollider2D> ().bounds.extents.y * 2;
		collisionDetectionBox.size = new Vector2(.1f, .1f);
	}

	// moves the box with which the player interacts with things
	public void rearrangeCollisionArea(int currentDirection) {
		float distanceFromPlayer = .30f;

		if(currentDirection == 0) {
			collisionDetector.transform.localPosition = new Vector2(-distanceFromPlayer, 0);
			collisionDetectionBox.size = new Vector2(.1f, boxHeight);
		} else if(currentDirection == 1) {
			collisionDetector.transform.localPosition = new Vector2(0, distanceFromPlayer);
			collisionDetectionBox.size = new Vector2(boxWidth, .1f);
		} else if(currentDirection == 2) {
			collisionDetector.transform.localPosition = new Vector2(distanceFromPlayer, 0);
			collisionDetectionBox.size = new Vector2(.1f, boxHeight);
		} else {
			collisionDetector.transform.localPosition = new Vector2(0, -distanceFromPlayer);
			collisionDetectionBox.size = new Vector2(boxWidth, .1f);
		}
	}
}

