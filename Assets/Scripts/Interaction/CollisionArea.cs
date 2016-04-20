using UnityEngine;
using System.Collections;

/**
 * An area that can be collided with that is
 * on the player and npcs
 */
public class CollisionArea : MonoBehaviour
{
	protected int currentDirection;					// the current direction being faced
	protected GameObject collisionDetector;			// the detector for the collision
	protected BoxCollider2D collisionDetectionBox;	// the box for detecting collisions

	// the area of the collision box
	protected float boxWidth;
	protected float boxHeight;

	// sets the collision area to be collided with
	public CollisionArea(GameObject parentCharacter) {
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
	public virtual void rearrangeCollisionArea(int currentDirection) {
		this.currentDirection = currentDirection;
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

	// the current direction that the box is facing
	public int CurrentDirection {
		get {return currentDirection;}
	}
}

