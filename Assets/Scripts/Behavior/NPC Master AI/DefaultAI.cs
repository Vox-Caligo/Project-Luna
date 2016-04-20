using UnityEngine;
using System.Collections;

/**
 * Default thought processes for any NPC
 */
public class DefaultAI : MasterBehavior
{
	protected PlayerCombat playerKarma;		// karma of the player
	protected Vector2 currentPosition;		// the current location
	protected Collision2D lastCollision;	// the last collision experienced
	protected bool inCutscene = false;		// if character is in a cutscene

	// initializes combat and movement to default values
	protected override void Start() {
		if(npcCombat == null)
			npcCombat = new DefaultCombatController(characterName, this.gameObject);

		if(npcMovement == null)
			npcMovement = new DefaultMovementController(characterName, this.gameObject);
	}

	// ai to be run by this character
	protected virtual void processDecisions() {
		// current location
		currentPosition = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

		// determine if actor should move, attack, or other
		// if move, determine how to move
		// if attack, determine how to attack

		// two special movements
		if(npcMovement.CurrentAction == "dash" || npcMovement.CurrentAction == "bounce") {
			if(npcMovement.InjureViaMovement) {
				// pass to combat apply damage (lastCollision does not contain the actual gameObject)
				npcCombat.applyAiAttackDamage(lastCollision.gameObject);
			}
		}

		// respond to player karma
		karmaReactions ();
	}

	// collects the last collision
	protected virtual void OnCollisionEnter2D (Collision2D col) {
		lastCollision = col;
		/*	if(npcCombat.InAttack 
		if(((NpcCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == characterName + " Attack") {
			((NpcCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		} // else if(((NpcMovement)characterMovement) check for dash or bounce and collision with player
		*/
	}

	// how to respond to player karma
	protected virtual void karmaReactions() {}

	// checks if the character can use ai and then runs its combat/movement scripts
	protected virtual void Update ()
	{
		// cannot use ai if in a cutscene
		if(!inCutscene) {
			processDecisions();
		}

		npcMovement.runScript();
		npcCombat.runScript(npcMovement.CurrentDirection);
	}

	// get/set for the character's health
	public override int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return npcCombat.characterHealth(); // change this to call the AI Combat and then proceed to call health
		} else {
			npcCombat.characterHealth(newHealthValue);
			return -1;
		}
	}

	// returns the characters movement controller
	public DefaultMovementController NpcMovement {
		get {return npcMovement;}
	}

	// get/set for whether the character is in a cutscene
	public bool InCutscene {
		get {return inCutscene;}
		set {inCutscene = value;}
	}
}

