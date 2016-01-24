using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	// movement variables
	private Animator animator;
	private int currDirection = 2;
	private Vector2 speed = new Vector2 (5, 5);

	void Start ()
	{
		animator = this.GetComponent<Animator> ();
	}
	
	// used to walk around the map as well as apply the correct animation
	void walk() {
		Vector2 movement;
		float turnAroundTime = -.2f;
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		if((inputX != 0 || inputY != 0) /*&& !attackScript.inAttack*/) {
			animator.SetBool ("Walking", true);
			animator.speed = 1;
			
			if(inputX != 0) {
				movement = new Vector2 (speed.x * inputX, 0);
				
				if (inputX > turnAroundTime) {
					animator.SetInteger ("Direction", 3);
					currDirection = 1;
				} else if (inputX < -turnAroundTime) {
					animator.SetInteger ("Direction", 1);
					currDirection = 0;
				}
			} else {
				movement = new Vector2 (0, speed.y * inputY);
				
				if (inputY > turnAroundTime) {
					animator.SetInteger ("Direction", 2);
					currDirection = 2;
				} else if (inputY < -turnAroundTime) {
					animator.SetInteger ("Direction", 0);
					currDirection = 3;
				}
			}
		} else {
			movement = new Vector2(0,0);
			animator.speed = 0;
			animator.SetBool ("Walking", false);
		}
		
		rigidbody2D.velocity = movement;
	}
	
	// used for movement
	void FixedUpdate() {
		walk ();
	}
	
	public int CurrDirection {
		get {return currDirection;}
	}
}