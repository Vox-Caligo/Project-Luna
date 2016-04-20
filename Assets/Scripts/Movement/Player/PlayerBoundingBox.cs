using UnityEngine;
using System.Collections;

/**
 * The box surrounding the player that can be collided with
 */
public class PlayerBoundingBox : MonoBehaviour
{
	// stores the player
	private GameObject player;

	// bounding box for the player
	private float playerLeftBound;
	private float playerRightBound;
	private float playerTopBound;
	private float playerBottomBound;

	// sets the player 
	public PlayerBoundingBox(GameObject player) {
		this.player = player;
	}

	// updates the bounding box that is surrounding the player
	public void updatePlayerBoundingBox() {
		playerLeftBound = player.transform.position.x - player.GetComponent<BoxCollider2D> ().bounds.extents.x;
		playerRightBound = player.transform.position.x + player.GetComponent<BoxCollider2D> ().bounds.extents.x;
		playerTopBound = player.transform.position.y + player.GetComponent<BoxCollider2D> ().bounds.extents.y;
		playerBottomBound = player.transform.position.y - player.GetComponent<BoxCollider2D> ().bounds.extents.y;
	}

	// left side of the collider
	public float PlayerLeftBound {
		get { return playerLeftBound; }
	}

	// right side of the collider
	public float PlayerRightBound {
		get { return playerRightBound; }
	}

	// top of the collider
	public float PlayerTopBound {
		get { return playerTopBound; }
	}

	// bottom of the collider
	public float PlayerBottomBound {
		get { return playerBottomBound; }
	}
}

