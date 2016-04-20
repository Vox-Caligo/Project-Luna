using UnityEngine;
using System.Collections;

/**
 * Determines when the player is colliding with an object
 * as well as when the player has exited said collision.
 * Also causes the effects of the collision to come to life
 * (mainly terrain features)
 */
public class DeterminingCollisionActions : MonoBehaviour
{
	// variables for the player
	private GameObject player;
	private PlayerMovement playerMovement;
	private PlayerBoundingBox currentBoundingBox;

	// variables for the terrain
	private TerrainPiece currentTerrain;
	private ColliderBoundingBox currentColliderBoundingBox;

	// grabs the player game object and the movement script
	public DeterminingCollisionActions(GameObject player, PlayerMovement playerMovement) {
		this.player = player;
		this.playerMovement = playerMovement;
		currentBoundingBox = new PlayerBoundingBox (this.player);
	}

	// checks the terrain and if it has a special effect
	public void checkIfActiveTerrain() {
		// makes sure terrain exists
		if(currentTerrain != null) {
			currentBoundingBox.updatePlayerBoundingBox ();

			// activaes terrain if on, otherwise turns terrain off
			if(determineIfCurrentlyColliding()) {
				activateTerrainFeature();
				playerMovement.TerrainIsActivated = true;
			} else {
				playerMovement.TerrainIsActivated = false;
			}
		}
	}

	// checks if the player is currently colliding with the area, this includes checking to make sure
	// the appropriate areas are on, whether it needs another piece of terrain to stop, and that it
	// looks visually correct when the player is stopping
	private bool determineIfCurrentlyColliding() {
		if(playerMovement.CurrentDirection == 0 && feetAreInBounds()) {
			if(playerMovement.IsFrictionStopNeeded) {
				if(currentBoundingBox.PlayerRightBound < currentColliderBoundingBox.ColliderRightBound) {
					return true;
				}
			} else if(currentBoundingBox.PlayerLeftBound < currentColliderBoundingBox.ColliderRightBound) {
				return true;
			}
		} else if(playerMovement.CurrentDirection == 1) {
			if(currentBoundingBox.PlayerBottomBound > currentColliderBoundingBox.ColliderBottomBound) {
				return true;
			}
		} else if(playerMovement.CurrentDirection == 2 && feetAreInBounds()) {
			if(playerMovement.IsFrictionStopNeeded) {
				if(currentBoundingBox.PlayerLeftBound > currentColliderBoundingBox.ColliderLeftBound) {
					return true;
				}
			} else if(currentBoundingBox.PlayerRightBound > currentColliderBoundingBox.ColliderLeftBound) {
				return true;
			}
		} else if(playerMovement.CurrentDirection == 3) {
			if(playerMovement.IsFrictionStopNeeded && currentBoundingBox.PlayerTopBound < currentColliderBoundingBox.ColliderTopBound) {
				return true;
			} else if(((currentTerrain != null && currentTerrain.isSlippery) || (currentTerrain.sender || currentTerrain.receiver)) && 
						currentBoundingBox.PlayerBottomBound < currentColliderBoundingBox.ColliderTopBound) {
				return true;
			}
		}
		return false;
	}

	// checks that the players feet are in the object since the perspective is slanted
	private bool feetAreInBounds() {
		if(currentBoundingBox.PlayerBottomBound < currentColliderBoundingBox.ColliderTopBound && 
			currentBoundingBox.PlayerBottomBound > currentColliderBoundingBox.ColliderBottomBound) {
			return true;
		}
		return false;
	}

	// activates the features of the current terrain
	private void activateTerrainFeature() {
		// checks that there is terrain with features
		if(currentTerrain != null) {
			// checks if the terrain causes sliding
			if (currentTerrain.isSlippery) {
				playerMovement.IsSliding = true;
			}

			// checks if the terrain is sliding and needs a stop piece
			if (currentTerrain.needsFrictionStop) {
				playerMovement.IsFrictionStopNeeded = true;
			}

			// checks if the terrain is a teleporter entrance
			if (currentTerrain.sender) {
				if (!currentTerrain.TeleporterOnFreeze && currentTerrain.isSisterAReceiver()) {
					playerMovement.teleport();
				}
			}

			// checks if the current terrain is the type to climb
			if(currentTerrain.climable) {
				playerMovement.IsClimbing = true;
			}
		} 
	}

	// when entering terrain, sets the current terrain and gets the new bounding box
	public void interpretEnteringCurrentTerrainTrigger(Collider2D col, TerrainPiece newTerrain) {
		currentTerrain = newTerrain;
		playerMovement.CurrentTerrain = currentTerrain;
		currentColliderBoundingBox = new ColliderBoundingBox (currentTerrain.gameObject);
	}

	// when exiting terrain, removes the current terrain piece
	public void interpretExitingCurrentTerrainTrigger(Collider2D col, TerrainPiece newTerrain) {
		currentTerrain = null;
		playerMovement.CurrentTerrain = new TerrainPiece();
	}

	// checks if colliding with a piece of terrain
	public void interpretCurrentTerrainCollider(Collision2D col) {
		if(col.gameObject.GetComponent<TerrainPiece>().isSturdy) {
			playerMovement.CollidingWithSturdyObject = true;
		}
	}
}