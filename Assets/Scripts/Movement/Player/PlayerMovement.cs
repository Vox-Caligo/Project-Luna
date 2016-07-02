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
    private Rigidbody2D playerRigidbody;

    // movement variables
    private int currentDirection = 0;
	private Vector2 speed = new Vector2 (5, 5);
    private float terrainModifier = 1;

    // the current terrain piece the player is on
    private TerrainPiece currentTerrain = new TerrainPiece();

	// ice/sliding
	private bool isSliding = false;
	private bool isFrictionStopNeeded = false;
	private bool collidingWithSturdyObject = false;
    private Vector2 slideValue;

    // climbing
    private bool isClimbing = false;

	// character is in the cutscene
	private bool inCutscene = false;

	// sets the player and the animator
	public PlayerMovement (GameObject player) {
		this.player = player;
		characterAnimator = new CharacterAnimator(this.player);
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

	public void updatePlayerMovement() {
		// checcks that the character is not in a cutscene
		if(!inCutscene) {
            if (isSliding) {
                // if sliding, stop when either colliding, leaving the terrain, or
                // hitting a friction stop (depending on what is required). Otherwise
                // have the character slide

                if (collidingWithSturdyObject) {
                    if (characterAnimator.isInMotion()) {
                        characterAnimator.stop();
                    }

                    int newCurrentDirection = checkIfMovingWhileSliding(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), currentDirection);

                    if (newCurrentDirection != -1) {
                        currentDirection = newCurrentDirection;
                        collidingWithSturdyObject = false;
                        playerRigidbody.velocity = new Vector2(speed.x * slideValue.x * terrainModifier, speed.y * slideValue.y * terrainModifier);
                        characterAnimator.walk(currentDirection);
                    }
                } else {
                    playerRigidbody.velocity = new Vector2(speed.x * slideValue.x * terrainModifier, speed.y * slideValue.y * terrainModifier);
                }
			} else {
				getPlayerInput();
			}
		}
	}
	
	// used to walk around the map as well as apply the correct animation
	private void getPlayerInput() {
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
            playerRigidbody.velocity = new Vector2((speed.x * inputX) * terrainModifier, (speed.y * inputY) * terrainModifier);
        } else {
			// stops all movement
			characterAnimator.stop();
		}
	}

    // check if the player is trying to move while sliding
    public int checkIfMovingWhileSliding(float inputX, float inputY, int currentDirection) {
        // moves a certain direction depending on input
        if (Mathf.Abs(inputX) > Mathf.Abs(inputY)) {
            if (inputX > 0 && currentDirection != 2) {
                slideValue = new Vector2(-1, 0);
                return 2;
            } else if (inputX < 0 && currentDirection != 0) {
                slideValue = new Vector2(1, 0);
                return 0;
            }
        } else {
            if (inputY > 0 && currentDirection != 1) {
                slideValue = new Vector2(0, 1);
                return 1;
            } else if (inputY < 0 && currentDirection != 3) {
                slideValue = new Vector2(0, -1);
                return 3;
            }
        }

        return -1;
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

    public float TerrainModifier {
        set { terrainModifier = value; }
    }

    // sets if the player is sliding
    public bool IsSliding {
        get { return isSliding; }
        set {isSliding = value;}
	}

    public Vector2 SlideValue {
        set { slideValue = value; }
    }

    // sets if the player is climbing
    public bool IsClimbing {
		set {isClimbing = value;}
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