using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	protected bool hostile = false;

	protected override void Start() {
		characterName = "Minion";
		base.Start ();
		npcCombat = new MinionCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
	}
	
	protected override void processDecisions ()
	{
		// will eventually go through and actually think things through
		npcMovement.CurrentAction = "flee";
		npcMovement.runScript();
		npcCombat.runScript();
		base.processDecisions();
	}

	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		npcCombat.respondToCollision(col);
	}
}

