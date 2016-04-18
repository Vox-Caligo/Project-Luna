using UnityEngine;
using System.Collections;

public class VillagerAI : DefaultAI
{
	protected bool hostile = false;

	protected override void Start() {
		characterName = "Villager";
		npcCombat = new VillagerCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
		playerKarma = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster>().currentCharacterCombat();
	}

	protected override void processDecisions ()
	{
		base.processDecisions();
	}

	protected override void karmaReactions() {
		// have it do things depending on players current karma
		if (playerKarma.Karma < 0) {
			npcMovement.TargetPoint = GameObject.FindGameObjectWithTag ("Player").transform.position;
			npcMovement.CurrentAction = "flee";
			npcCombat.CurrentAction = "";
		} else {
			npcMovement.CurrentAction = "halt";
			npcCombat.CurrentAction = "";
		}
	}

	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		npcCombat.respondToCollision(col);
	}
}

