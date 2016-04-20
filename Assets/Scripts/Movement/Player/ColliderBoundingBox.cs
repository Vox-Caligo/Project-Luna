using UnityEngine;
using System.Collections;

/**
 * A box that can be collided with
 */
public class ColliderBoundingBox : MonoBehaviour
{
	// bounding box for the collider
	private float colliderLeftBound;
	private float colliderRightBound;
	private float colliderTopBound;
	private float colliderBottomBound;

	// sets the collider box that can be collided with
	public ColliderBoundingBox(GameObject collider) {
		colliderLeftBound = collider.transform.position.x - collider.GetComponent<BoxCollider2D> ().bounds.extents.x;
		colliderRightBound = collider.transform.position.x + collider.GetComponent<BoxCollider2D> ().bounds.extents.x;
		colliderTopBound = collider.transform.position.y + collider.GetComponent<BoxCollider2D> ().bounds.extents.y;
		colliderBottomBound = collider.transform.position.y - collider.GetComponent<BoxCollider2D> ().bounds.extents.y;
	}

	// left side of the collider
	public float ColliderLeftBound {
		get { return colliderLeftBound; }
	}

	// right side of the collider
	public float ColliderRightBound {
		get { return colliderRightBound; }
	}

	// top of the collider
	public float ColliderTopBound {
		get { return colliderTopBound; }
	}

	// bottom of the collider
	public float ColliderBottomBound {
		get { return colliderBottomBound; }
	}
}

