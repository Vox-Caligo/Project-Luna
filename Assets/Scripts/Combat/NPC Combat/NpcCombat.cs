using UnityEngine;
using System.Collections;

public class NpcCombat : MonoBehaviour
{
	private DefaultCombatController npcCombat;
	
	// Use this for initialization
	public NpcCombat (string characterName, GameObject character) {
		switch(characterName) {
		case "Minion":
			npcCombat = new MinionCombatController(characterName, character);
			break;
		default:
			npcCombat = new DefaultCombatController(characterName, character);
			break;
		}
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		//npcCombat.runScript();
	}
}