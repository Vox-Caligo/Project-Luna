using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	public override void Start() {
		hostile = true;
		base.Start ();
		characterMovement = new NpcMovement(characterName, this.gameObject);
		print ("DA FUCK: " + characterMovement);
	}
	
	protected override void processDecisions ()
	{
		//print ("I am a minion making minion decisions");

		// initiate dash
		print("Why: " + characterMovement.npcMovement);
		characterMovement.npcMovement.CurrentAction = "flee";
		characterMovement.npcMovement.runScript();
	}
}

