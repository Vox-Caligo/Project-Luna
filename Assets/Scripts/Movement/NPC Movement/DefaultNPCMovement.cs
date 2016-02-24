using UnityEngine;
using System.Collections;

public class NpcMovement : CharacterMovementController {
	public DefaultMovementController npcMovement;
	
	// Use this for initialization
	public NpcMovement (string npcMovementType, GameObject character) {
		print("Not getting here");
		switch(npcMovementType) {
		case "Minion":
			npcMovement = new MinionMovementController(npcMovementType, character);
			break;
		default:
			npcMovement = new DefaultMovementController(npcMovementType, character);
			break;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		npcMovement.runScript();
	}
}
