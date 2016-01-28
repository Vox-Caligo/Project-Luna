using UnityEngine;
using System.Collections;

public class NpcMovement : MonoBehaviour {
	
	public string npcMovementType;
	private DefaultMovement npcMovement;
	
	// Use this for initialization
	void Start () {
		switch(npcMovementType) {
		case "Minion":
			npcMovement = new MinionMovement(this.gameObject);
			break;
		default:
			npcMovement = new DefaultMovement(this.gameObject);
			break;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		npcMovement.runScript();
	}
}
