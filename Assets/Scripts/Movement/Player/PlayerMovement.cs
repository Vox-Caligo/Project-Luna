using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovementController {
	// movement variables
	private GameObject player;
	private Animator animator;
	private int currentDirection = 0;
	private Vector2 speed = new Vector2 (5, 5);
	private TerrainPiece currentTerrain = new TerrainPiece();
	private Teleporter currentTeleporter = new Teleporter();
	private bool isSliding = false;
	private bool isFrictionStopNeeded = false;
	private DeterminingCollisionActions determiningCollisions;

	public PlayerMovement (GameObject player) {
		this.player = player;
		animator = this.player.GetComponent<Animator>();
		determiningCollisions = new DeterminingCollisionActions(this.player);
	}

	public void updatePlayerMovement() {
		bool terrainIsActivated = determiningCollisions.shouldTerrainColliderActivate(currentDirection, isFrictionStopNeeded);

		if (terrainIsActivated) {
			activateTerrainFeature ();
		}

		if(!isSliding) {
			walk();
		} else if(isSliding) {
			if (currentTerrain.isFrictionStop && terrainIsActivated) {
				isSliding = false;
				isFrictionStopNeeded = false;
			} else if(!isFrictionStopNeeded && isSliding != currentTerrain.isSlippery) {
				isSliding = currentTerrain.isSlippery;
			} else {
				slide ();
			}
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

	public void interpretCurrentTerrain(Collider2D col, BaseTerrain newTerrain) {
		print ("hm");
		determiningCollisions.setNewTerrain(newTerrain);

		if(newTerrain.GetType().Name == "TerrainPiece") {
			currentTerrain = (TerrainPiece)newTerrain;
			currentTeleporter = new Teleporter ();
		} else if(newTerrain.GetType().Name == "Teleporter") {
			currentTeleporter = (Teleporter)newTerrain;
			currentTerrain = new TerrainPiece();
		} else {
			currentTerrain = new TerrainPiece();
			currentTeleporter = new Teleporter ();
		}
	}

	private void activateTerrainFeature() {
		if (currentTerrain.isSlippery) {
			isSliding = true;
		}

		if (currentTerrain.needsFrictionStop) {
			isFrictionStopNeeded = true;
		}

		if (currentTeleporter.sender) {
			if (!currentTeleporter.TeleporterOnFreeze && currentTeleporter.isSisterAReceiver ()) {
				player.transform.position = currentTeleporter.teleportCoordinates ();
			}
		}
	}

	public int CurrentDirection {
		get {return currentDirection;}
	}
}