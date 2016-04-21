using UnityEngine;
using System.Collections;

/**
 * Determines how the player moves and what actions occur during
 * said movement.
 */
public class PlayerMovement : CharacterMovementController {
	// player variables
	private GameObject player;
	private CharacterAnimator characterAnimator;

	// movement variables
	private int currentDirection = 0;
	private Vector2 speed = new Vector2 (5, 5);

	// the current terrain piece the player is on
	private TerrainPiece currentTerrain = new TerrainPiece();

	// terrain attributed movement
	private bool terrainIsActivated = false;

	// ice/sliding
	private bool isSliding = false;
	private bool isFrictionStopNeeded = false;
	private bool collidingWithSturdyObject = false;

	// climbing
	private bool isClimbing = false;

	// character is in the cutscene
	private bool inCutscene = false;

	// sets the player and the animator
	public PlayerMovement (GameObject player) {
		this.player = player;
		characterAnimator = new CharacterAnimator(this.player);
	}

	public void updatePlayerMovement() {
		// checcks that the character is not in a cutscene
		if(!inCutscene) {
			if(isSliding) {
				// if sliding, stop when either colliding, leaving the terrain, or
				// hitting a friction stop (depending on what is required). Otherwise
				// have the character slide
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
				// if the terrain is climable, make the player climb
				isClimbing = currentTerrain.climable;
			} else {
				walk();
			}
		}
	}
	
	// used to walk around the map as well as apply the correct animation
	private void walk() {
		// gets input from the player
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");

		// depending on input, moves the player
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
				// animates walking
				characterAnimator.walk(currentDirection);
			} else {
				// animates climbing
				characterAnimator.walk(1);
			}

			// moves the character
			applyMovement(new Vector2 ((speed.x * inputX), (speed.y * inputY)));
		} else {
			// stops all movement
			characterAnimator.stop();
		}
	}

	// has the player slide until out of the terrain/hits friction stop
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

	// check if the player is trying to move while sliding
	private void checkIfMovingWhileSliding() {
		// stops the characters animation
		if(characterAnimator.isInMotion()) {
			characterAnimator.stop();
		}

		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		bool givenInput = false;

		// moves a certain direction depending on input
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

		// moves the character if input was given
		if(givenInput) {
			characterAnimator.walk(currentDirection);
			collidingWithSturdyObject = false;
		}
	}

	// applies movement to the player
	private void applyMovement(Vector2 calculatedMovement) {
		float terrainModifier = 1;

		// if the player is being slown down
		if(currentTerrain.isSlowdown) {
			terrainModifier = currentTerrain.slowdownSpeed;
		}

		// if the player is being sped up
		if(currentTerrain.isSpeedup) {
			terrainModifier = currentTerrain.speedupSpeed;
		}

		// moves the player
		player.GetComponent<Rigidbody2D>().velocity = new Vector2 (calculatedMovement.x * terrainModifier, calculatedMovement.y * terrainModifier);
	}

	// teleports the player to a receiver teleport
	public void teleport() {
		player.transform.position = currentTerrain.teleportCoordinates ();
		int newTeleportDirection = currentTerrain.isSisterADirectional();

		if(newTeleportDirection != -1) {
			currentDirection = newTeleportDirection;
			characterAnimator.walk(currentDirection);
		}
	}

	// get/set the player's current direction
	public int CurrentDirection {
		get {return currentDirection;}
		set {currentDirection = value;}
	}

	// sets if the player is colliding with sturdy terrain
	public bool CollidingWithSturdyObject {
		set {collidingWithSturdyObject = value;}
	}

	// sets the current terrain the player is on
	public TerrainPiece CurrentTerrain {
		set {currentTerrain = value;}
	}

	// sets if the player is sliding
	public bool IsSliding {
		set {isSliding = value;}
	}

	// sets if the player is climbing
	public bool IsClimbing {
		set {isClimbing = value;}
	}

	// sets the active terrain the player is on
	public bool TerrainIsActivated {
		set {terrainIsActivated = value;}
	}

	// get/set if the  player needs friction terrain to stop
	public bool IsFrictionStopNeeded {
		get {return isFrictionStopNeeded;}
		set {isFrictionStopNeeded = value;}
	}

	// get the player's animator
	public CharacterAnimator CharacterAnimator {
		get {return characterAnimator;}
	}

	// get/set if the player is in a cutscene
	public bool InCutscene {
		get { return inCutscene; }
		set { inCutscene = value; }
	}
}