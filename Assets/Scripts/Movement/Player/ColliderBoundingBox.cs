using UnityEngine;
using System.Collections;

public class ColliderBoundingBox : MonoBehaviour
{
	// bounding box for the collider
	private float colliderLeftBound;
	private float colliderRightBound;
	private float colliderTopBound;
	private float colliderBottomBound;

	public ColliderBoundingBox(GameObject collider) {
		colliderLeftBound = collider.transform.position.x - collider.GetComponent<BoxCollider2D> ().bounds.extents.x;
		colliderRightBound = collider.transform.position.x + collider.GetComponent<BoxCollider2D> ().bounds.extents.x;
		colliderTopBound = collider.transform.position.y + collider.GetComponent<BoxCollider2D> ().bounds.extents.y;
		colliderBottomBound = collider.transform.position.y - collider.GetComponent<BoxCollider2D> ().bounds.extents.y;
	}

	public float ColliderLeftBound {
		get { return colliderLeftBound; }
	}

	public float ColliderRightBound {
		get { return colliderRightBound; }
	}

	public float ColliderTopBound {
		get { return colliderTopBound; }
	}

	public float ColliderBottomBound {
		get { return colliderBottomBound; }
	}
}

