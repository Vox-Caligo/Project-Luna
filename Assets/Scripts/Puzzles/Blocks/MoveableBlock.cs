using UnityEngine;
using System.Collections;

public class MoveableBlock : InteractableItem
{
	public bool slideable;
	public bool pickupable;

	private bool beingCarried = false;
	private BoxCollider2D myCollider;
	private GameObject pickupArea;
	private PlayerMovement player;
	private int pickedUpDirection;
	private int currentDirection;
	private float[] rotationArray;

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

	protected void FixedUpdate () {
		// rotate item when player moves
		if(beingCarried) {
			this.transform.position = pickupArea.transform.position;

			if(currentDirection != player.CurrentDirection) {
				itemHasBeenRotated(player.CurrentDirection);
			}
		}
	}

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

	private void itemHasBeenRotated(int newDirection) {
		gameObject.transform.Rotate(0, 0, rotationArray[newDirection]);
		currentDirection = newDirection;
		setRotationArray(currentDirection);
	}

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
}

