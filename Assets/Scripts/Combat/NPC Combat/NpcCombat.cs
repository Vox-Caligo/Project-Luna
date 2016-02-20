using UnityEngine;
using System.Collections;

public class NpcCombat : Combat
{
	private DefaultCombat npcCombat;
	
	// Use this for initialization
	public NpcCombat (string characterName, GameObject character) : base(characterName, character) {
		switch(characterName) {
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