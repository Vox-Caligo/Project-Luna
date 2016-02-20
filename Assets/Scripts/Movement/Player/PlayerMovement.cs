﻿using UnityEngine;
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
	private void walk() {
		Vector2 movement = new Vector2(0,0);
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		
		if((inputX != 0 || inputY != 0) /*&& !attackScript.inAttack*/) {
			animator.SetBool ("Walking", true);
			animator.speed = 1;
			
			if(Mathf.Abs(inputX) > Mathf.Abs(inputY)) {
				if (inputX > 0) {
					animator.SetInteger ("Direction", 3);
					currDirection = 1;
				} else {
					animator.SetInteger ("Direction", 1);
					currDirection = 0;
				}
			} else {
				if (inputY > 0) {
					animator.SetInteger ("Direction", 2);
					currDirection = 2;
				} else {
					animator.SetInteger ("Direction", 0);
					currDirection = 3;
				}
			}
			
			movement = new Vector2 (speed.x * inputX, speed.y * inputY);
		} else {
			animator.speed = 0;
			animator.SetBool ("Walking", false);
		}
		
		GetComponent<Rigidbody2D>().velocity = movement;
	}
	
	// used for movement
	void FixedUpdate() {
		walk ();
	}
	
	public int CurrDirection {
		get {return currDirection;}
	}
}