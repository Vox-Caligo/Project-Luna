using UnityEngine;
using System.Collections;

/**
 * The AI of a minion npc
 */
public class MinionAI : DefaultAI
{
	protected bool hostile = false;

	// create a minion's combat and movement
	protected override void Start() {
		characterName = "Minion";
		npcCombat = new MinionCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
	}

	// run through it's thought process
	protected override void processDecisions ()
	{
		// will eventually go through and actually think things through
		npcMovement.TargetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

		// pursue the player until close enough, then decide to attack

		// if far away head towards the player, if close then attack
		if(Vector2.Distance(currentPosition, npcMovement.TargetPoint) > 1.5f/*this.gameObject.GetComponent<BoxCollider2D>().bounds.*/) {
			npcMovement.CurrentAction = "pursue";
			npcCombat.CurrentAction = "";
		} else {
			npcMovement.CurrentAction = "nearby-player";
			npcCombat.CurrentAction = "attack";
		}

		// testing purposes
		npcMovement.CurrentAction = "halt";
		npcCombat.CurrentAction = "";

		base.processDecisions();
	}

	// how to respond to player karma
	protected override void karmaReactions() {
		// have it do things depending on players current karma
	}

	// how to respond to collisions
	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		npcCombat.respondToCollision(col);
	}
}

