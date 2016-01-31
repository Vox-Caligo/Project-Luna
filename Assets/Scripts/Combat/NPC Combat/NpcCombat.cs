using UnityEngine;
using System.Collections;

public class NpcCombat : MonoBehaviour
{
	private DefaultCombat npcCombat;
	
	// Use this for initialization
	public NpcCombat (string npcCombatType) {
		switch(npcCombatType) {
		case "Minion":
			npcCombat = new MinionCombat();
			break;
		default:
			npcCombat = new DefaultCombat();
			break;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		npcCombat.runScript();
	}
}