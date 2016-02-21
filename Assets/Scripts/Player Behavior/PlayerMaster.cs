using UnityEngine;
using System.Collections;

public class PlayerMaster : MonoBehaviour {
	private PlayerCombat playerCombat;
	private PlayerMovement playerMovement;
	private int currentDirection = 0;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	public virtual void Start() {
		playerCombat = new PlayerCombat("Player", this.gameObject);
		playerMovement = new PlayerMovement(this.gameObject);
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		playerMovement.walk();
		playerCombat.updatePlayerCombat();
	}

	void OnCollisionEnter2D(Collision2D col) {
		// determine if it was while attacking
			// check if in attack and if it was the attack box that was collided with
			// otherwise take damage (unless something special shows up)
		if(playerCombat.InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			//if(col.contacts[0].collider.GetComponent<DefaultAI>())
			// check if other collided object is an NPC and has an AI
			// think about putting this in combat
			print("Hitting " + col.contacts[0].collider.name);
		} else {

		}
		//playerCombat
		//characterMovement.npcMovement.respondToCollision (col);
		// do same for combat
	}
}
