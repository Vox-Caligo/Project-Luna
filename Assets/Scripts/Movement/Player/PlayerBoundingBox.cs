using UnityEngine;
using System.Collections;

public class PlayerBoundingBox : MonoBehaviour
{
	private GameObject player;

	// bounding box for the player
	private float playerLeftBound;
	private float playerRightBound;
	private float playerTopBound;
	private float playerBottomBound;

	public PlayerBoundingBox(GameObject player) {
		this.player = player;
	}

	public void updatePlayerBoundingBox() {
		playerLeftBound = player.transform.position.x - player.GetComponent<BoxCollider2D> ().bounds.extents.x;
		playerRightBound = player.transform.position.x + player.GetComponent<BoxCollider2D> ().bounds.extents.x;
		playerTopBound = player.transform.position.y + player.GetComponent<BoxCollider2D> ().bounds.extents.y;
		playerBottomBound = player.transform.position.y - player.GetComponent<BoxCollider2D> ().bounds.extents.y;
	}

	public float PlayerLeftBound {
		get { return playerLeftBound; }
	}

	public float PlayerRightBound {
		get { return playerRightBound; }
	}

	public float PlayerTopBound {
		get { return playerTopBound; }
	}

	public float PlayerBottomBound {
		get { return playerBottomBound; }
	}
}

