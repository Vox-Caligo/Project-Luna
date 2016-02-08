using UnityEngine;
using System.Collections;

public class NpcMovement : MonoBehaviour {
	public DefaultMovement npcMovement;
	
	// Use this for initialization
	public NpcMovement (string npcMovementType, GameObject character) {
		switch(npcMovementType) {
		case "Minion":
			npcMovement = new MinionMovement(character);
			break;
		default:
			npcMovement = new DefaultMovement(character);
			break;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		npcMovement.runScript();
	}
}
