using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	public override void Start() {
		hostile = true;
		base.Start ();
	}
	
	protected override void processDecisions ()
	{
		//print ("I am a minion making minion decisions");

		// initiate dash
		characterMovement.npcMovement.CurrentAction = "flee";
		characterMovement.npcMovement.runScript();
	}
}

