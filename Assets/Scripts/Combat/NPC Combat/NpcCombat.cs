using UnityEngine;
using System.Collections;

public class NpcCombat : MonoBehaviour
{
	private DefaultCombat npcCombat;
	
	// Use this for initialization
	public NpcCombat (string characterName, GameObject character) {
		switch(characterName) {
		case "Minion":
			npcCombat = new MinionCombat(characterName, character);
			break;
		default:
			npcCombat = new DefaultCombat(characterName, character);
			break;
		}
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		//npcCombat.runScript();
	}
}