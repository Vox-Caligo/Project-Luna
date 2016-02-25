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
		npcMovement.CurrentAction = "flee";
		npcMovement.runScript();
	}
}

