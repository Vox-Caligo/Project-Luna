using UnityEngine;
using System.Collections;

public class DeterminingCollisionActions : MonoBehaviour
{
	private GameObject player;
	private PlayerMovement playerMovement;
	private PlayerBoundingBox currentBoundingBox;

	private TerrainPiece currentTerrain;
	private Teleporter currentTeleporter;
	private ColliderBoundingBox currentColliderBoundingBox;

	public DeterminingCollisionActions(GameObject player, PlayerMovement playerMovement) {
		this.player = player;
		this.playerMovement = playerMovement;
		currentBoundingBox = new PlayerBoundingBox (this.player);
	}

	public void checkIfActiveTerrain() {
		if(currentTerrain != null || currentTeleporter != null) {
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
			} else if(((currentTerrain != null && currentTerrain.isSlippery) || currentTeleporter != null) && currentBoundingBox.PlayerBottomBound < currentColliderBoundingBox.ColliderTopBound) {
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
		} else if(currentTeleporter != null) {
			print("hm");
			if (currentTeleporter.sender) {
				if (!currentTeleporter.TeleporterOnFreeze && currentTeleporter.isSisterAReceiver()) {
					playerMovement.teleport();
				}
			}
		}
	}

	public void interpretEnteringCurrentTerrainTrigger(Collider2D col, BaseTerrain newTerrain) {
		if (newTerrain.GetType ().Name == "TerrainPiece") {
			currentTerrain = (TerrainPiece)newTerrain;
			playerMovement.CurrentTerrain = currentTerrain;
			currentTeleporter = null;
			currentColliderBoundingBox = new ColliderBoundingBox (currentTerrain.gameObject);
		} else if(newTerrain.GetType().Name == "Teleporter") {
			currentTeleporter = (Teleporter)newTerrain;
			playerMovement.CurrentTeleporter = currentTeleporter;
			currentTerrain = null;
			currentColliderBoundingBox = new ColliderBoundingBox (currentTeleporter.gameObject);
		} else {
			currentTeleporter = null;
			currentTerrain = null;
			playerMovement.CurrentTerrain = new TerrainPiece();
			playerMovement.CurrentTeleporter = new Teleporter();
		}
	}

	public void interpretExitingCurrentTerrainTrigger(Collider2D col, BaseTerrain newTerrain) {
		currentTeleporter = null;
		currentTerrain = null;
		playerMovement.CurrentTerrain = new TerrainPiece();
		playerMovement.CurrentTeleporter = new Teleporter();
	}

	public void interpretCurrentTerrainCollider(Collision2D col) {
		if(col.gameObject.GetComponent<TerrainPiece>().isSturdy) {
			playerMovement.CollidingWithSturdyObject = true;
		}
	}
}