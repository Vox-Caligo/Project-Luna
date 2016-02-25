﻿using UnityEngine;
using System.Collections;

public class PlayerMaster : MasterBehavior {
	// know when/where to move
	// know when to attack

	// Use this for initialization
	protected override void Start() {
		characterCombat = new PlayerCombat("Player", this.gameObject);
		characterMovement = new PlayerMovement(this.gameObject);
	}

	// Update is called once per frame
	protected void FixedUpdate ()
	{
		((PlayerMovement)characterMovement).walk();
		((PlayerCombat)characterCombat).updatePlayerCombat(((PlayerMovement)characterMovement).CurrDirection);
	}

	protected override void OnCollisionEnter2D(Collision2D col) {
		if(((PlayerCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			((PlayerCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		}
	}
}
