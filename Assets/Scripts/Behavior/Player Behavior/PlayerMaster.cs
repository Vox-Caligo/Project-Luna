using UnityEngine;
using System.Collections;

public class PlayerMaster : MasterBehavior {
	private string playerWeapon = "Sword";	// get current weapon
	private InteractionArea interactableArea;
	private DeterminingCollisionActions determiningCollisions;
	private KeyboardInput keyChecker;

	private bool beAwareOfChildColliders = false;
	private int collidingPieces = 0;

	// Use this for initialization
	protected override void Start() {
		characterMovement = new PlayerMovement(this.gameObject);
		interactableArea = new InteractionArea(this.gameObject);
		characterCombat = new PlayerCombat("Player", this.gameObject, playerWeapon);
		determiningCollisions = new DeterminingCollisionActions(this.gameObject, ((PlayerMovement)characterMovement));
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();
	}

	// Update is called once per frame
	protected void FixedUpdate () {
		determiningCollisions.checkIfActiveTerrain();
		((PlayerMovement)characterMovement).updatePlayerMovement();
		interactableArea.rearrangeCollisionArea(((PlayerMovement)characterMovement).CurrentDirection);
		((PlayerCombat)characterCombat).updatePlayerCombat(((PlayerMovement)characterMovement).CurrentDirection);
	}

	protected override void OnCollisionEnter2D(Collision2D col) {
		if(((PlayerCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == "Player Attack") {
			((PlayerCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		} else if(col.gameObject.GetComponent<TerrainPiece>() != null) {
			determiningCollisions.interpretCurrentTerrainCollider(col);
		} else if(col.gameObject.GetComponent<MoveableBlock>() != null) {
			col.gameObject.GetComponent<MoveableBlock>().collidedWithCharacter(((PlayerMovement)characterMovement).CurrentDirection);
		}
	}

	protected void OnTriggerStay2D(Collider2D col) {
		if(keyChecker.useKey(KeyCode.E)) {
			if(col.gameObject.GetComponent<MoveableBlock>() != null) {
				col.gameObject.GetComponent<MoveableBlock>().onInteractionWithMovable(((PlayerMovement)characterMovement));
			} else if(col.gameObject.GetComponent<InteractableItem>() != null) {
				col.gameObject.GetComponent<InteractableItem>().onInteraction();
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.GetComponent<TerrainPiece> () != null) {
			collidingPieces++;
			if (collidingPieces == this.gameObject.GetComponentsInChildren<BoxCollider2D> ().Length) {
				determiningCollisions.interpretEnteringCurrentTerrainTrigger (col, col.gameObject.GetComponent<TerrainPiece> ());
				beAwareOfChildColliders = true;
			}
		} else if (col.gameObject.GetComponent<CutsceneTrigger> () != null) {
			CutsceneTrigger triggeredScene = col.gameObject.GetComponent<CutsceneTrigger> ();

			if (!triggeredScene.CutsceneActivated) {
				triggeredScene.startCutscene ();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col) {
		beAwareOfChildColliders = false;

		if(col.gameObject.GetComponent<TerrainPiece>() != null) {
			if(collidingPieces > this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length) {
				collidingPieces = this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length;
			} 

			if(collidingPieces > 0) {
				collidingPieces--;
			}
			if(collidingPieces == 0) {
				TerrainPiece currentTerrain = col.gameObject.GetComponent<TerrainPiece>();

				if(currentTerrain != null && currentTerrain.receiver) {
					currentTerrain.TeleporterOnFreeze = false;
				}

				determiningCollisions.interpretExitingCurrentTerrainTrigger(col, col.gameObject.GetComponent<TerrainPiece>());
			}
		}
	}
}
