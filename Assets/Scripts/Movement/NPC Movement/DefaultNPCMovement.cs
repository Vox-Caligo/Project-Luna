using UnityEngine;
using System.Collections;

public class NpcMovement : CharacterMovement {
	public DefaultMovement npcMovement;
	
	// Use this for initialization
	public NpcMovement (string npcMovementType, GameObject character) {
		print("Not getting here");
		switch(npcMovementType) {
		case "Minion":
			npcMovement = new MinionMovement(npcMovementType, character);
			break;
		default:
			npcMovement = new DefaultMovement(npcMovementType, character);
			break;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		npcMovement.runScript();
	}
}
