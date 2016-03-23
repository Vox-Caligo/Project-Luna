using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovementController {
	// movement variables
	private GameObject player;
	private CharacterAnimator characterAnimator;

	private int currentDirection = 0;
	private Vector2 speed = new Vector2 (5, 5);

	private TerrainPiece currentTerrain = new TerrainPiece();

	// terrain attributed movement
	private bool terrainIsActivated = false;

	// ice/sliding
	private bool isSliding = false;
	private bool isFrictionStopNeeded = false;
	private bool collidingWithSturdyObject = false;

	// climbing
	private bool isClimbing = false;

	public PlayerMovement (GameObject player) {
		this.player = player;
		characterAnimator = new CharacterAnimator(this.player);
	}

	public void updatePlayerMovement() {
		if(isSliding) {
			if(collidingWithSturdyObject) {
				checkIfMovingWhileSliding();
			} else {
				if (currentTerrain.isFrictionStop && terrainIsActivated) {
					isSliding = false;
					isFrictionStopNeeded = false;
				} else if(!isFrictionStopNeeded && isSliding != currentTerrain.isSlippery) {
					isSliding = currentTerrain.isSlippery;
				} else {
					slide ();
				}
			}
		} else if(isClimbing != currentTerrain.climable) {
			isClimbing = currentTerrain.climable;
		} else {
			walk();
		}
	}
	
	// used to walk around the map as well as apply the correct animation
	private void walk() {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		if((inputX != 0 || inputY != 0) /*&& !attackScript.inAttack*/) {			
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

			if(!isClimbing) {
				characterAnimator.walk(currentDirection);
			} else {
				characterAnimator.walk(1);
			}

			applyMovement(new Vector2 ((speed.x * inputX), (speed.y * inputY)));
		} else {
			characterAnimator.stop();
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

	private void checkIfMovingWhileSliding() {
		if(characterAnimator.isInMotion()) {
			characterAnimator.stop();
		}

		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		bool givenInput = false;

		if(Mathf.Abs(inputX) > Mathf.Abs(inputY)) {
			if (inputX > 0 && currentDirection != 2) {
				currentDirection = 2;
				givenInput = true;
			} else if (inputX < 0 && currentDirection != 0){
				currentDirection = 0;
				givenInput = true;
			}
		} else {
			if (inputY > 0 && currentDirection != 1) {
				currentDirection = 1;
				givenInput = true;
			} else if (inputY < 0 && currentDirection != 3){
				currentDirection = 3;
				givenInput = true;
			}
		}

		if(givenInput) {
			characterAnimator.walk(currentDirection);
			collidingWithSturdyObject = false;
		}
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

	public void teleport() {
		player.transform.position = currentTerrain.teleportCoordinates ();
		int newTeleportDirection = currentTerrain.isSisterADirectional();

		if(newTeleportDirection != -1) {
			currentDirection = newTeleportDirection;
			characterAnimator.walk(currentDirection);
		}
	}

	public int CurrentDirection {
		get {return currentDirection;}
		set {currentDirection = value;}
	}

	public bool CollidingWithSturdyObject {
		set {collidingWithSturdyObject = value;}
	}

	public TerrainPiece CurrentTerrain {
		set {currentTerrain = value;}
	}

	public bool IsSliding {
		set {isSliding = value;}
	}

	public bool IsClimbing {
		set {isClimbing = value;}
	}

	public bool TerrainIsActivated {
		set {terrainIsActivated = value;}
	}

	public bool IsFrictionStopNeeded {
		get {return isFrictionStopNeeded;}
		set {isFrictionStopNeeded = value;}
	}
}