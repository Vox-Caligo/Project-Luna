using UnityEngine;
using System.Collections;

/**
 * A block that may be picked up and/or slide around
 * and everything that that entails
 */
public class MoveableBlock : InteractableItem
{
	// sets whether slideable or pickupable
	public bool slideable;
	public bool pickupable;

	// group the button is in
	public int buttonGroup;

	// variables for being picked up
	private bool beingCarried = false;
	private BoxCollider2D myCollider;
	private GameObject pickupArea;

	// variables for direction rotation
	private PlayerMovement player;
	private int pickedUpDirection;
	private int currentDirection;

	// array that holds how to rotate when a new direction is chosen
	private float[] rotationArray;

	// checks the interaction and can be picked up and/or slid around
	public override void onInteractionWithMovable(PlayerMovement player) { 
		if(myCollider == null) {
			this.player = player;
			myCollider = this.GetComponent<BoxCollider2D>();
			pickupArea = GameObject.Find("Player Interaction Box");
		}

		if(pickupable) {
			beingCarried = !beingCarried;

			// have a minor time delay between picking up and dropping
			if(beingCarried) {
				pickedUpDirection = this.player.CurrentDirection;
				currentDirection = pickedUpDirection;
				setRotationArray(pickedUpDirection);
				myCollider.isTrigger = true;
			} else {
				rotationArray = null;
				myCollider.isTrigger = false;
			}
		}
	}

	// checks when the item is being carried if it is rotated
	protected void Update () {
		// rotate item when player moves
		if(beingCarried) {
			this.transform.position = pickupArea.transform.position;

			if(currentDirection != player.CurrentDirection) {
				itemHasBeenRotated(player.CurrentDirection);
			}
		}
	}

	// if it has collided with the player, then it moves
	public void collidedWithCharacter(int playerDirection) {
		float currentX = this.gameObject.transform.position.x;
		float currentY = this.gameObject.transform.position.y;

		if(playerDirection == 0) {
			currentX -= .1f;
		} else if(playerDirection == 1) {
			currentY += .1f;
		} else if(playerDirection == 2) {
			currentX += .1f;
		} else {
			currentY -= .1f;
		}

		this.gameObject.transform.position = new Vector3(currentX, currentY);
	}

	// checks if the item has been rotated, rotates, and resets the rotation array
	private void itemHasBeenRotated(int newDirection) {
		gameObject.transform.Rotate(0, 0, rotationArray[newDirection]);
		currentDirection = newDirection;
		setRotationArray(currentDirection);
	}

	// sets values that rotates the piece when picked up and moved a different direction
	private void setRotationArray(int newDirection) {
		if(newDirection == 0) {
			rotationArray = new float[4] {0, 270, 180, 90};
		} else if(newDirection == 1) {
			rotationArray = new float[4] {90, 0, 270, 180};
		} else if(newDirection == 2) {
			rotationArray = new float[4] {180, 90, 0, 270};
		} else {
			rotationArray = new float[4] {270, 180, 90, 0};
		}
	}

	public bool BeingCarried {
		get { return beingCarried; }
	}
}

