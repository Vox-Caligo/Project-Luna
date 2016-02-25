using UnityEngine;
using System.Collections;

public class DefaultCombatController : MonoBehaviour
{
	protected DefaultNPCCombat npcCombat;
	protected string characterName;
	
	// Use this for initialization
	public DefaultCombatController (string characterName, GameObject character) {
		this.characterName = characterName;

		switch(characterName) {
		case "Minion":
			npcCombat = new DefaultNPCCombat(characterName, character); //MinionCombat();
			break;
		default:
			npcCombat = new DefaultNPCCombat(characterName, character);
			break;
		}
	}
	
	public virtual void runScript() { }

	public void respondToCollision(Collision2D col) {
		if(npcCombat.InAttack && col.contacts[0].otherCollider.name == characterName + " Attack") {
			npcCombat.applyAttackDamage(col.contacts [0].collider.gameObject);
			//((NpcCombat)npcCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		}
	}

	public int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return npcCombat.Health;
		} else {
			npcCombat.Health = newHealthValue;
			return -1;
		}
	}

	public void applyAiAttackDamage(GameObject targetCharacter) {
		npcCombat.applyAttackDamage(targetCharacter);
	}
}

