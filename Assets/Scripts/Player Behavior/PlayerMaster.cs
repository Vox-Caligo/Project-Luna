using UnityEngine;
using System.Collections;

public class PlayerMaster : DefaultAI {
	private int currentDirection = 0;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	public override void Start() {
		characterCombat = new PlayerCombat("Player", this.gameObject);
		characterMovement = new PlayerMovement(this.gameObject);
		print ("CM: " + characterMovement);
	}

	// Update is called once per frame
	protected override void FixedUpdate ()
	{
		characterMovement.walk();
		characterCombat.updatePlayerCombat();
	}

	protected override void OnCollisionEnter2D(Collision2D col) {
		// determine if it was while attacking
			// check if in attack and if it was the attack box that was collided with
			// otherwise take damage (unless something special shows up)
		if(characterCombat.InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			//if(col.contacts[0].collider.GetComponent<DefaultAI>())
			// check if other collided object is an NPC and has an AI
			// think about putting this in combat
			characterCombat.applyAttackDamage (col.contacts [0].collider.gameObject);
		} else {

		}
		//playerCombat
		//characterMovement.npcMovement.respondToCollision (col);
		// do same for combat
	}
}
