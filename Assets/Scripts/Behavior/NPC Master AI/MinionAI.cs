using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	protected bool hostile = false;

	protected override void Start() {
		characterName = "Minion";
		base.Start ();
	}
	
	protected override void processDecisions ()
	{
		npcMovement.CurrentAction = "flee";
		npcMovement.runScript();
	}
}

