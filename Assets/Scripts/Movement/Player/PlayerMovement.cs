using UnityEngine;
using System.Collections;

public class PlayerMovement : CharacterMovementController {
	// movement variables
	private GameObject player;
	private Animator animator;
	private int currentDirection = 3;
	private Vector2 speed = new Vector2 (5, 5);

	public PlayerMovement (GameObject player) {
		this.player = player;
		animator = this.player.GetComponent<Animator>();
	}
	
	// used to walk around the map as well as apply the correct animation
	public void walk() {
		Vector2 movement = new Vector2(0,0);
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		if((inputX != 0 || inputY != 0) /*&& !attackScript.inAttack*/) {
			animator.SetBool ("Walking", true);
			animator.speed = 1;
			
			if(Mathf.Abs(inputX) > Mathf.Abs(inputY)) {
				if (inputX > 0) {
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
			
			movement = new Vector2 (speed.x * inputX, speed.y * inputY);
		} else {
			animator.speed = 0;
			animator.SetBool ("Walking", false);
		}
		
		player.GetComponent<Rigidbody2D>().velocity = movement;
	}
	
	public int CurrDirection {
		get {return currentDirection;}
	}
}