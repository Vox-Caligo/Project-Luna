using UnityEngine;
using System.Collections;

public class DefaultAI : MasterBehavior
{
	protected Collision2D lastCollision;

	// Use this for initialization
	protected override void Start() {
		npcCombat = new DefaultCombatController(characterName, this.gameObject);
		npcMovement = new DefaultMovementController(characterName, this.gameObject);
	}

	protected virtual void processDecisions() {
		// determine if actor should move, attack, or other
		// if move, determine how to move
		// if attack, determine how to attack
		if(npcMovement.CurrentAction == "dash" || npcMovement.CurrentAction == "bounce") {
			if(npcMovement.InjureViaMovement) {
				// pass to combat apply damage (lastCollision does not contain the actual gameObject)
				npcCombat.applyAiAttackDamage(lastCollision.gameObject);
			}
		}
	}

	protected virtual void OnCollisionEnter2D (Collision2D col) {
		lastCollision = col;
		/*	if(npcCombat.InAttack 
		if(((NpcCombat)characterCombat).InAttack && col.contacts[0].otherCollider.name == characterName + " Attack") {
			((NpcCombat)characterCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		} // else if(((NpcMovement)characterMovement) check for dash or bounce and collision with player
		*/
	}

	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		processDecisions();
	}

	public override int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return npcCombat.characterHealth(); // change this to call the AI Combat and then proceed to call health
		} else {
			npcCombat.characterHealth(newHealthValue);
			return -1;
		}
	}
}

