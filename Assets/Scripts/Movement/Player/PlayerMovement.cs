using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovementController {
	// movement variables
	private GameObject player;
	private Animator animator;
	private int currentDirection = 2;
	private Vector2 speed = new Vector2 (5, 5);
	private TerrainPiece currentTerrain = new TerrainPiece();
	private bool isSliding = false;

	public PlayerMovement (GameObject player) {
		this.player = player;
		animator = this.player.GetComponent<Animator>();
	}

	public void updatePlayerMovement() {
		if(isSliding != currentTerrain.isSlippery) {
			isSliding = currentTerrain.isSlippery;
		}

		if(!isSliding) {
			walk();
		} else {
			slide();
		}
	}
	
	// used to walk around the map as well as apply the correct animation
	private void walk() {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		if((inputX != 0 || inputY != 0) /*&& !attackScript.inAttack*/) {
			animator.SetBool ("Walking", true);
			animator.speed = 1;
			
			if(Mathf.Abs(inputX) > Mathf.Abs(inputY)) {
				if (inputX > 0) {
					currentDirection = 2;
				} else {
					currentDirection = 0;
				}
			} else {
				if (inputY > 0) {
					currentDirection = 1;
				} else {
					currentDirection = 3;
				}
			}

			animator.SetInteger ("Direction", currentDirection);

			applyMovement(new Vector2 ((speed.x * inputX), (speed.y * inputY)));
		} else {
			animator.speed = 0;
			animator.SetBool ("Walking", false);
		}
	}

	// has the player slide until out of the terrain
	private void slide() {
		int xVelocity = 0, yVelocity = 0;

		if(currentDirection == 0) {
			xVelocity = -1;
		} else if(currentDirection == 1) {
			yVelocity = 1;
		} else if (currentDirection == 2) {
			xVelocity = 1;
		} else {
			yVelocity = -1;
		}

		applyMovement(new Vector2 ((speed.x * xVelocity), (speed.y * yVelocity)));
	}

	// applies movement to the player
	private void applyMovement(Vector2 calculatedMovement) {
		float terrainModifier = 1;

		if(currentTerrain.isSlowdown) {
			terrainModifier = currentTerrain.slowdownSpeed;
		}

		if(currentTerrain.isSpeedup) {
			terrainModifier = currentTerrain.speedupSpeed;
		}


		player.GetComponent<Rigidbody2D>().velocity = new Vector2 (calculatedMovement.x * terrainModifier, calculatedMovement.y * terrainModifier);
	}

	public void interpretCurrentTerrain(BaseTerrain newTerrain) {
		if(newTerrain.GetType().Name == "TerrainPiece") {
			currentTerrain = (TerrainPiece)newTerrain;

			if(currentTerrain.isSlippery) {
				isSliding = true;
			}

			if(currentTerrain.isFrictionStop) {
				isSliding = false;
			}
		} else if(newTerrain.GetType().Name == "Teleporter") {
			Teleporter currentTeleport = (Teleporter)newTerrain;

			if(!currentTeleport.TeleporterOnFreeze && currentTeleport.sender && currentTeleport.isSisterAReceiver()) {
				player.transform.position = currentTeleport.teleportCoordinates();
			}
		} else {
			currentTerrain = new TerrainPiece();
		}
	}

	public int CurrentDirection {
		get {return currentDirection;}
	}
}