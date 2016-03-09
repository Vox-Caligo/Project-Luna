using UnityEngine;
using System.Collections;

public class PlayerMaster : MasterBehavior {
	private string playerWeapon = "Sword";	// get current weapon
	private InteractionArea interactableArea;

	private bool beAwareOfChildColliders = false;
	private int collidingPieces = 0;

	// Use this for initialization
	protected override void Start() {
		characterMovement = new PlayerMovement(this.gameObject);
		interactableArea = new InteractionArea(this.gameObject);
		characterCombat = new PlayerCombat("Player", this.gameObject, playerWeapon);
	}

	// Update is called once per frame
	protected void FixedUpdate () {
		((PlayerMovement)characterMovement).updatePlayerMovement();
		interactableArea.rearrangeCollisionArea(((PlayerMovement)characterMovement).CurrentDirection);
		((PlayerCombat)characterCombat).updatePlayerCombat(((PlayerMovement)characterMovement).CurrentDirection);
	}

	protected override void OnCollisionEnter2D(Collision2D col) {
		if(((PlayerCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			((PlayerCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		}
	}

	protected void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.GetComponent<InteractableItem>() != null && Input.GetKeyDown(KeyCode.E)) {
			col.gameObject.GetComponent<InteractableItem>().onInteraction();
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<BaseTerrain>() != null) {
			collidingPieces++;
			if(collidingPieces == this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length) {
				((PlayerMovement)characterMovement).interpretCurrentTerrain(col, col.gameObject.GetComponent<BaseTerrain>());
				beAwareOfChildColliders = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col) {
		beAwareOfChildColliders = false;

		if(col.gameObject.GetComponent<BaseTerrain>() != null) {
			if(collidingPieces > this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length) {
				collidingPieces = this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length;
			} 

			if(collidingPieces > 0) {
				collidingPieces--;
			}
			if(collidingPieces == 0) {
				if(col.gameObject.GetComponent<Teleporter>() != null && col.gameObject.GetComponent<Teleporter>().receiver) {
					col.gameObject.GetComponent<Teleporter>().TeleporterOnFreeze = false;
				}

				((PlayerMovement)characterMovement).interpretCurrentTerrain(col, new BaseTerrain());
			}
		}
	}
}
