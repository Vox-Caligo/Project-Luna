using UnityEngine;
using System.Collections;

public class InteractionArea : MonoBehaviour
{
	private GameObject interactionArea;

	// Use this for initialization
	public InteractionArea(GameObject player)
	{
		interactionArea = new GameObject();
		interactionArea.transform.parent = player.transform;
		interactionArea.transform.position = interactionArea.transform.parent.position;
		interactionArea.name = "Player Interaction Box";
		interactionArea.AddComponent<InteractableItem>();

		BoxCollider2D interactionBox = interactionArea.AddComponent<BoxCollider2D>();
		interactionBox.isTrigger = true;
		interactionBox.size = new Vector2(.1f, .1f);
	}
	
	protected void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.GetComponent<InteractableItem>() != null && Input.GetKeyDown(KeyCode.E)) {
			col.gameObject.GetComponent<InteractableItem>().onInteraction();
		}
	}

	// moves the box with which the player interacts with things
	public void rearrangeInteractionArea(int currentDirection) {
		float distanceFromPlayer = .30f;

		if(currentDirection == 0) {
			interactionArea.transform.localPosition = new Vector2(-distanceFromPlayer, 0);
		} else if(currentDirection == 1) {
			interactionArea.transform.localPosition = new Vector2(0, distanceFromPlayer);
		} else if(currentDirection == 2) {
			interactionArea.transform.localPosition = new Vector2(distanceFromPlayer, 0);
		} else {
			interactionArea.transform.localPosition = new Vector2(0, -distanceFromPlayer);
		}
	}
}

