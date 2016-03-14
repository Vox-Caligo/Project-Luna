using UnityEngine;
using System.Collections;

public class DeterminingCollisionActions : MonoBehaviour
{
	private GameObject player;
	private PlayerMovement playerMovement;
	private PlayerBoundingBox currentBoundingBox;

	private TerrainPiece currentTerrain;
	private ColliderBoundingBox currentColliderBoundingBox;

	public DeterminingCollisionActions(GameObject player, PlayerMovement playerMovement) {
		this.player = player;
		this.playerMovement = playerMovement;
		currentBoundingBox = new PlayerBoundingBox (this.player);
	}

	public void checkIfActiveTerrain() {
		if(currentTerrain != null) {
			currentBoundingBox.updatePlayerBoundingBox ();

			if(determineIfCurrentlyColliding()) {
				activateTerrainFeature();
				playerMovement.TerrainIsActivated = true;
			} else {
				playerMovement.TerrainIsActivated = false;
			}
		}
	}

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

	private bool feetAreInBounds() {
		if(currentBoundingBox.PlayerBottomBound < currentColliderBoundingBox.ColliderTopBound && 
			currentBoundingBox.PlayerBottomBound > currentColliderBoundingBox.ColliderBottomBound) {
			return true;
		}
		return false;
	}

	private void activateTerrainFeature() {
		if(currentTerrain != null) {
			if (currentTerrain.isSlippery) {
				playerMovement.IsSliding = true;
			}

			if (currentTerrain.needsFrictionStop) {
				playerMovement.IsFrictionStopNeeded = true;
			}

			if (currentTerrain.sender) {
				if (!currentTerrain.TeleporterOnFreeze && currentTerrain.isSisterAReceiver()) {
					playerMovement.teleport();
				}
			}

			if(currentTerrain.climable) {
				playerMovement.IsClimbing = true;
			}
		} 
	}

	public void interpretEnteringCurrentTerrainTrigger(Collider2D col, TerrainPiece newTerrain) {
		currentTerrain = newTerrain;
		playerMovement.CurrentTerrain = currentTerrain;
		currentColliderBoundingBox = new ColliderBoundingBox (currentTerrain.gameObject);
	}

	public void interpretExitingCurrentTerrainTrigger(Collider2D col, TerrainPiece newTerrain) {
		currentTerrain = null;
		playerMovement.CurrentTerrain = new TerrainPiece();
	}

	public void interpretCurrentTerrainCollider(Collision2D col) {
		if(col.gameObject.GetComponent<TerrainPiece>().isSturdy) {
			playerMovement.CollidingWithSturdyObject = true;
		}
	}
}