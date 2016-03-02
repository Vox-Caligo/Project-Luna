using UnityEngine;
using System.Collections;

public class PlayerMaster : MasterBehavior {
	private string playerWeapon = "Sword";	// get current weapon
	private InteractableItem interactableItem;

	// Use this for initialization
	protected override void Start() {
		characterCombat = new PlayerCombat("Player", this.gameObject, playerWeapon);
		characterMovement = new PlayerMovement(this.gameObject);
	}

	// Update is called once per frame
	protected void FixedUpdate () {
		((PlayerMovement)characterMovement).updatePlayerMovement();
		((PlayerCombat)characterCombat).updatePlayerCombat(((PlayerMovement)characterMovement).CurrentDirection);

		// press button, check if item is in front of player
		if(Input.GetKeyDown(KeyCode.E)) {
			
		}
	}

	protected override void OnCollisionEnter2D(Collision2D col) {
		if(((PlayerCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			((PlayerCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D col) {
		InteractableItem possibleInteractableItem = col.gameObject.GetComponent<InteractableItem>();
		if(possibleInteractableItem != null && Input.GetKeyDown(KeyCode.E)) {
			possibleInteractableItem.onInteraction();
		}
	}
}
