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
    private SoundInterpreter sounds;

	// timer that saves the current player to storage
	private UtilTimer autoSave;
	private int timeDelay = 150000; // 2.5 min

	// used for colliding with other area
	private bool beAwareOfChildColliders = false;
	private int collidingPieces = 0;

	// a block to pick up
	private PickupableBlock carryingMovableObject;

    // test for quest log adding
    private int nextQuestLogAdd = 0;

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
		playerHud = new PlayerHUD(this.gameObject.name, ((PlayerCombat)characterCombat).Health.CurrentHealth, ((PlayerCombat)characterCombat).Mana.CurrentMana);
		playerInventory = new Inventory(storage.retrievePlayerInventory());
		playerQuests = new QuestLog ();
		autoSave = new UtilTimer (1, 1); // replace with timeDelay when not testing
        sounds = new SoundInterpreter(this.gameObject);
        sounds.playSound("Test Theme", false, true);
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
			playerHud.hudUpdate(((PlayerCombat)characterCombat).Health.CurrentHealth, ((PlayerCombat)characterCombat).Mana.CurrentMana);

			// shows the player inventory
			if(keyChecker.useKey(KeyCode.Q)) {
				playerInventory.visibility();
            }

            // shows the player quest log
            if (keyChecker.useKey(KeyCode.R)) {
                playerQuests.changeQuestLogVisibility();
            }

            // testing using a useable item
            if (keyChecker.useKey(KeyCode.V)) {
                playerQuests.changeQuestLogVisibility();
            }

            // check for passive items

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
			//playerInventory.addItemFromInventory("Starter Sword", "Weapon");
			//playerInventory.addItemFromInventory("Starter Axe", "Weapon");
            playerInventory.addItemFromInventory("Regenerative Health Potion", "Regenerative Health Potion");
            playerInventory.addItemFromInventory("Regenerative Mana Potion", "Regenerative Mana Potion");
            playerInventory.addItemFromInventory("Regenerative Health Potion", "Regenerative Health Potion");
            playerInventory.addItemFromInventory("Regenerative Mana Potion", "Regenerative Mana Potion");
            playerInventory.addItemFromInventory("Regenerative Health Potion", "Regenerative Health Potion");
            playerInventory.addItemFromInventory("Regenerative Mana Potion", "Regenerative Mana Potion");
            playerInventory.addItemFromInventory("Regenerative Health Potion", "Regenerative Health Potion");
            playerInventory.addItemFromInventory("Regenerative Mana Potion", "Regenerative Mana Potion");
            //playerInventory.addItemFromInventory("Regenerative Mana Potion", "Regenerative Mana Potion");
        }

		// test to remove items
		if(keyChecker.useKey(KeyCode.P)) {
			playerInventory.removeItemFromInventory("Starter Sword");
			playerInventory.removeItemFromInventory("Starter Axe");
            playerInventory.removeItemFromInventory("Instant Health Potion");
            playerInventory.removeItemFromInventory("Instant Mana Potion");
            playerInventory.removeItemFromInventory("Regenerative Health Potion");
            playerInventory.removeItemFromInventory("Regenerative Mana Potion");

            playerInventory.removeItemFromInventory("Regenerative Health Potion");
            playerInventory.removeItemFromInventory("Regenerative Mana Potion");
            playerInventory.removeItemFromInventory("Regenerative Health Potion");
            playerInventory.removeItemFromInventory("Regenerative Mana Potion");
            playerInventory.removeItemFromInventory("Regenerative Health Potion");
            playerInventory.removeItemFromInventory("Regenerative Mana Potion");
            playerInventory.removeItemFromInventory("Regenerative Health Potion");
            playerInventory.removeItemFromInventory("Regenerative Mana Potion");
        }

        playerInventory.updateRegenerativePotions();
	}

	// checks for collisions being entered
	protected override void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.GetComponent<TerrainPiece>() != null) {
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

    // returns the player's quest log
    public QuestLog PlayerQuests {
        get { return playerQuests; }
    }

    // returns the player's hud
    public PlayerHUD PlayerHud {
        get { return playerHud; }
    }
}
