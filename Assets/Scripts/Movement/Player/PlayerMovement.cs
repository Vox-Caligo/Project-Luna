using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovementController {
	// movement variables
	private GameObject player;
	private Animator animator;
	private int currentDirection = 1;
	private Vector2 speed = new Vector2 (5, 5);
	private TerrainPiece currentTerrain = new TerrainPiece();

	public PlayerMovement (GameObject player) {
		this.player = player;
		animator = this.player.GetComponent<Animator>();
	}

	public void updatePlayerMovement() {
		if(!currentTerrain.isSlippery) {
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
				if (inputX < 0) {
					animator.SetInteger ("Direction", 0);	// go left
					currentDirection = 0;
				} else {
					animator.SetInteger ("Direction", 2);	// go right
					currentDirection = 2;
				}
			} else {
				if (inputY > 0) {
					animator.SetInteger ("Direction", 1);	// go up
					currentDirection = 1;
				} else {
					animator.SetInteger ("Direction", 3);	// go down
					currentDirection = 3;
				}
			}

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
			xVelocity = 1;
		} else if(currentDirection == 1) {
			yVelocity = 1;
		} else if (currentDirection == 2) {
			xVelocity = -1;
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

	public TerrainPiece CurrentTerrain {
		set {currentTerrain = value;}
	}
	
	public int CurrentDirection {
		get {return currentDirection;}
	}
}