using UnityEngine;
using System.Collections;

public class InteractionArea : CollisionArea
{
		// Use this for initialization
	public InteractionArea(GameObject player) : base(player)
	{
		collisionDetector.name = "Player Interaction Box";
	}
	
	protected void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.GetComponent<InteractableItem>() != null && Input.GetKeyDown(KeyCode.E)) {
			col.gameObject.GetComponent<InteractableItem>().onInteraction();
		}
	}
}

