using UnityEngine;
using System.Collections;

/**
 * A block that may slide around
 * and everything that that entails
 */
public class SlidingBlock : MoveableBlock
{
	// ice/sliding
	private TerrainPiece currentTerrain = new TerrainPiece();
	private bool isSliding = false;
	private bool isFrictionStopNeeded = false;
	private bool collidingWithSturdyObject = false;

	// the currently pushed direction
	private int currentDirection = 0;

	// checks when the item is being carried if it is rotated
	protected void Update () {
		if(isSliding) {
			slide ();
		}
	}

	private void OnCollisionEnter2D(Collision2D col) {
		print ("Touching " + col.contacts[0].collider.name);
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.GetComponent<TerrainPiece> () != null) {
			currentTerrain = col.gameObject.GetComponent<TerrainPiece> ();

			if (currentTerrain.getTerrainType () == "slippery terrain") {
				isSliding = true;
			}

			if (currentTerrain.getTerrainType () == "friction terrain") {
				isSliding = false;
				this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
			}

			if (currentTerrain.getTerrainType () == "sturdy terrain") {
				print ("Stop");
				isSliding = false;
				this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
			}

			if (currentTerrain.getTerrainType () == "teleporter terrain") {
				if (!((TeleporterTerrain)currentTerrain).TeleporterOnFreeze && ((TeleporterTerrain)currentTerrain).isSisterAReceiver ()) {
					teleport ();
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.GetComponent<TerrainPiece> () != null) {
		
		}
	}

	// teleports the block to a receiver teleport
	private void teleport() {
		this.gameObject.transform.position = ((TeleporterTerrain)currentTerrain).teleportCoordinates ();
		int newTeleportDirection = ((TeleporterTerrain)currentTerrain).isSisterADirectional();

		if(newTeleportDirection != -1) {
			currentDirection = newTeleportDirection;
		}
	}

	// has the block slide until out of the terrain/hits friction stop
	private void slide() {
		float xVelocity = 0, yVelocity = 0;

		if(currentDirection == 0) {
			xVelocity = -2f;
		} else if(currentDirection == 1) {
			yVelocity = 2f;
		} else if (currentDirection == 2) {
			xVelocity = 2f;
		} else {
			yVelocity = -2f;
		}

		this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (xVelocity, yVelocity);
	}

	// if it has collided with the player, then it moves
	public void collidedWithCharacter(int playerDirection) {
		float currentX = this.gameObject.transform.position.x;
		float currentY = this.gameObject.transform.position.y;
		currentDirection = playerDirection;

		if(playerDirection == 0) {
			currentX -= .1f;
		} else if(playerDirection == 1) {
			currentY += .1f;
		} else if(playerDirection == 2) {
			currentX += .1f;
		} else {
			currentY -= .1f;
		}

		this.gameObject.transform.position = new Vector3(currentX, currentY);
	}
}