using UnityEngine;
using System.Collections;

public class DeterminingCollisionActions : MonoBehaviour
{
	private GameObject player;
	private PlayerBoundingBox currentBoundingBox;

	private TerrainPiece currentTerrain = new TerrainPiece();
	private ColliderBoundingBox currentColliderBoundingBox;

	public DeterminingCollisionActions(GameObject player) {
		this.player = player;
		currentBoundingBox = new PlayerBoundingBox (this.player);
	}

	public bool shouldTerrainColliderActivate(int currentDirection, bool isFrictionStopNeeded) {
		if(currentTerrain != null) {
			currentBoundingBox.updatePlayerBoundingBox ();
			return determineIfCurrentlyColliding(currentDirection, isFrictionStopNeeded);
		} else {
			return false;
		}
	}

	private bool determineIfCurrentlyColliding(int currentDirection, bool isFrictionStopNeeded) {
		if(currentDirection == 0 && feetAreInBounds()) {
			if(isFrictionStopNeeded) {
				if(currentBoundingBox.PlayerRightBound < currentColliderBoundingBox.ColliderRightBound) {
					return true;
				}
			} else if(currentBoundingBox.PlayerLeftBound < currentColliderBoundingBox.ColliderRightBound) {
				return true;
			}
		} else if(currentDirection == 1) {
			if(currentBoundingBox.PlayerBottomBound > currentColliderBoundingBox.ColliderBottomBound) {
				return true;
			}
		} else if(currentDirection == 2 && feetAreInBounds()) {
			if(isFrictionStopNeeded) {
				if(currentBoundingBox.PlayerLeftBound > currentColliderBoundingBox.ColliderLeftBound) {
					return true;
				}
			} else if(currentBoundingBox.PlayerRightBound > currentColliderBoundingBox.ColliderLeftBound) {
				return true;
			}
		} else if(currentDirection == 3) {
			if(isFrictionStopNeeded && currentBoundingBox.PlayerTopBound < currentColliderBoundingBox.ColliderTopBound) {
				return true;
			} else if(currentTerrain.isSlippery && currentBoundingBox.PlayerBottomBound < currentColliderBoundingBox.ColliderTopBound) {
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

	public void setNewTerrain(BaseTerrain newTerrain) {
		if (newTerrain.GetType ().Name == "TerrainPiece") {
			currentTerrain = (TerrainPiece)newTerrain;
			currentColliderBoundingBox = new ColliderBoundingBox (currentTerrain.gameObject);
		}
	}
}

