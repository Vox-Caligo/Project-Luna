using UnityEngine;
using System.Collections;

/**
 * A block that may be picked up
 * and everything that that entails
 */
public class PickupableBlock : MoveableBlock
{
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

	// changes what order the object is created on
	private void changeLayer() {
		if (currentDirection == 1) {
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
		} else if (currentDirection == 3) {
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
		}
	}

	// checks if the item has been rotated, rotates, and resets the rotation array
	private void itemHasBeenRotated(int newDirection) {
		gameObject.transform.Rotate(0, 0, rotationArray[newDirection]);
		currentDirection = newDirection;
		setRotationArray(currentDirection);
		changeLayer ();
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

