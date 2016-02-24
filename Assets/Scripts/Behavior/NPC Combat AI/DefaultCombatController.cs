using UnityEngine;
using System.Collections;

public class DefaultCombatController : MonoBehaviour
{
	protected DefaultNPCCombat npcCombat;
	
	// Use this for initialization
	public DefaultCombatController (string characterName, GameObject character) {
		switch(characterName) {
		case "Minion":
			npcCombat = new DefaultNPCCombat(characterName, character); //MinionCombat();
			break;
		default:
			npcCombat = new DefaultNPCCombat(characterName, character);
			break;
		}
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
		// think things
	}

	public int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return npcCombat.Health;
		} else {
			npcCombat.Health = newHealthValue;
			return -1;
		}
	}
}

