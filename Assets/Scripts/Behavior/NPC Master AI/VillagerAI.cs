using UnityEngine;
using System.Collections;

/**
 * The AI of a simple villager NPC
 */
public class VillagerAI : DefaultAI
{
	protected bool hostile = false;

	// set the combat, movement, and check the players karma
	protected override void Start() {
		characterName = "Villager";
		npcCombat = new VillagerCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
		playerKarma = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster>().currentCharacterCombat();
	}

	// run through ai
	protected override void processDecisions ()
	{
		base.processDecisions();
	}

	// how to respond to the players current karma level
	protected override void karmaReactions() {
		// have it do things depending on players current karma
		if (playerKarma.Karma < 0) {
			// flee if negative karma
			npcMovement.TargetPoint = GameObject.FindGameObjectWithTag ("Player").transform.position;
			npcMovement.CurrentAction = "flee";
			npcCombat.CurrentAction = "";
		} else {
			// do nothing otherwise
			npcMovement.CurrentAction = "halt";
			npcCombat.CurrentAction = "";
		}
	}

	// how to respond to a collision
	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		npcCombat.respondToCollision(col);
	}
}

