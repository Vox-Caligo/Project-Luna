using UnityEngine;
using System.Collections;

/**
 * Controls everything having to do with the player. This includes
 * the players combat and movement, as well as interactions with 
 * other objects (like puzzle pieces).
 */
public class PlayerMaster : MasterBehavior {
	private string playerWeapon = "Starter Sword";	// get current weapon
	private InteractionArea interactableArea;		// an area that can be interacted with
	private DeterminingCollisionActions determiningCollisions;	// determines what happens with collisions
	private KeyboardInput keyChecker;	// checks keys that have been interacted with
	private PlayerHUD playerHud;		// shows player status
	private Inventory playerInventory;	// the inventory that the player has
	private QuestLog playerQuests;		// stores information about currently active quests 
	private PlayerStorage storage;		// stores information for different play sessions

	// timer that saves the current player to storage
	private UtilTimer autoSave;
	private int timeDelay = 150000; // 2.5 min

	// used for colliding with other area
	private bool beAwareOfChildColliders = false;
	private int collidingPieces = 0;

	// a block to pick up
	private PickupableBlock carryingMovableObject;

	// Use this for initialization
	protected override void Start() {
		base.Start ();
		// pull values from storage
		storage = new PlayerStorage ();
		karma = storage.retrievePlayerKarma ();

		// sets everything else up
		characterMovement = new PlayerMovement(this.gameObject);
		interactableArea = new InteractionArea(this.gameObject);
		characterCombat = new PlayerCombat("Player", this.gameObject, karma, playerWeapon);
		determiningCollisions = new DeterminingCollisionActions(this.gameObject, ((PlayerMovement)characterMovement), ((PlayerCombat)characterCombat));
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();
		playerHud = new PlayerHUD(this.gameObject.name, ((PlayerCombat)characterCombat).Health, ((PlayerCombat)characterCombat).Mana);
		playerInventory = new Inventory(storage.retrievePlayerInventory());
		playerQuests = new QuestLog ();
		autoSave = new UtilTimer (1, 1); // replace with timeDelay when not testing
	}

	// Updates all lower ai (movement, inventory, combat)
	protected void FixedUpdate () {
		determiningCollisions.checkIfActiveTerrain ();
		((PlayerMovement)characterMovement).updatePlayerMovement ();
		interactableArea.rearrangeCollisionArea (((PlayerMovement)characterMovement).CurrentDirection);

		// checks if the player is in a cutscene
		if (!((PlayerMovement)characterMovement).InCutscene) {
			// updates player combat
			((PlayerCombat)characterCombat).updatePlayerCombat (((PlayerMovement)characterMovement).CurrentDirection, keyChecker.useKey(KeyCode.Space));

			// updates player hud
			playerHud.hudUpdate(((PlayerCombat)characterCombat).Health, ((PlayerCombat)characterCombat).Mana);

			// shows the player inventory
			if(keyChecker.useKey(KeyCode.Q)) {
				playerInventory.visibility();
			}

		} else {
			// hides inventory during cutscenes
			if(!playerInventory.inventoryIsInvisible()) {
				playerInventory.visibility();
			}
		}

		//this is a running timer countdownn that autosaves ;)
		if (autoSave.runningTimerCountdown()) {
			storage.storePlayer (((PlayerCombat)characterCombat).Karma, playerInventory.storeInventory ());
		}

		// test to add items
		if(keyChecker.useKey(KeyCode.O)) {
			playerInventory.addItemFromInventory("Starter Sword", "Weapon");
			playerInventory.addItemFromInventory("Starter Axe", "Weapon");
		}

		// test to remove items
		if(keyChecker.useKey(KeyCode.P)) {
			playerInventory.removeItemFromInventory("Starter Sword");
			playerInventory.removeItemFromInventory("Starter Axe");
		}
	}

	// checks for collisions being entered
	protected override void OnCollisionEnter2D(Collision2D col) {
		if(col.contacts[0].otherCollider.name == "Player Attack") {
			// checks if the player attack is colliding with an NPC
			((PlayerCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		} else if(col.gameObject.GetComponent<TerrainPiece>() != null) {
			// checks if the player is colliding with terrain
			determiningCollisions.interpretCurrentTerrainCollider(col);
		} else if(col.gameObject.GetComponent<SlidingBlock>() != null) {
			// checks if the player is interacting with a sliding object
			col.gameObject.GetComponent<SlidingBlock>().collidedWithCharacter(((PlayerMovement)characterMovement).CurrentDirection);
		}
	}

	// checks when a trigger is stayed in
	protected void OnTriggerStay2D(Collider2D col) {
		// interacts with an object when the E key is pressed
		if(keyChecker.useKey(KeyCode.E)) {
			// carryingMovableObject
			if(col.gameObject.GetComponent<PickupableBlock>() != null) {
				print ("Hi");
				if (carryingMovableObject == null) {
					carryingMovableObject = col.gameObject.GetComponent<PickupableBlock> ();
					carryingMovableObject.onInteractionWithMovable(((PlayerMovement)characterMovement));
				} else {
					carryingMovableObject.onInteractionWithMovable(((PlayerMovement)characterMovement));
					carryingMovableObject = null;
				}
			} else if(col.gameObject.GetComponent<InteractableItem>() != null) {
				col.gameObject.GetComponent<InteractableItem>().onInteraction();
			}
		}
	}

	// if the player enters a trigger
	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.GetComponent<TerrainPiece> () != null) {
			// checks if a terrain piece is being entered
			collidingPieces++;
			if (collidingPieces == this.gameObject.GetComponentsInChildren<BoxCollider2D> ().Length) {
				determiningCollisions.interpretEnteringCurrentTerrainTrigger (col, col.gameObject.GetComponent<TerrainPiece> ());
				beAwareOfChildColliders = true;
			}
		} else if (col.gameObject.GetComponent<CutsceneTrigger> () != null) {
			// checks if a cutscene should play
			CutsceneTrigger triggeredScene = col.gameObject.GetComponent<CutsceneTrigger> ();

			if (!triggeredScene.CutsceneActivated) {
				triggeredScene.startCutscene ();
			}
		}
	}

	// if the player exits a trigger
	private void OnTriggerExit2D(Collider2D col) {
		beAwareOfChildColliders = false;

		// determines what to do based on collision with terrain
		if(col.gameObject.GetComponent<TerrainPiece>() != null) {
			if(collidingPieces > this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length) {
				collidingPieces = this.gameObject.GetComponentsInChildren<BoxCollider2D>().Length;
			} 

			// removes pieces that are colliding
			if(collidingPieces > 0) {
				collidingPieces--;
			}

			// checks collisions with terrain 
			if(collidingPieces == 0) {
				TerrainPiece currentTerrain = col.gameObject.GetComponent<TerrainPiece>();

				if(currentTerrain.getTerrainType() == "teleporter terrain" && ((TeleporterTerrain)currentTerrain).receiver) {
					((TeleporterTerrain)currentTerrain).TeleporterOnFreeze = false;
				}

				determiningCollisions.interpretExitingCurrentTerrainTrigger(col, col.gameObject.GetComponent<TerrainPiece>());
			}
		}
	}

	// returns the players current version of movement
	public PlayerMovement currentCharacterMovement() {
		return ((PlayerMovement)characterMovement);
	}

	// returns the players current version of combat
	public PlayerCombat currentCharacterCombat() {
		return ((PlayerCombat)characterCombat);
	}

	// returns the items from the players inventory
	public Inventory PlayerInventory {
		get { return playerInventory; }
	}
}
