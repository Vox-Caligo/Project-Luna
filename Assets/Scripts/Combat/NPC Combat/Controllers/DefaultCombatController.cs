using UnityEngine;
using System.Collections;

public class DefaultCombatController : MonoBehaviour
{
	protected DefaultNpcCombat npcCombat;
	protected string characterName;
	protected string currentAction = "";
	
	// Use this for initialization
	public DefaultCombatController (string characterName, GameObject character) {
		this.characterName = characterName;

		switch(characterName) {
		case "Minion":
			npcCombat = new DefaultNpcCombat(characterName, character, "Sword"); //MinionCombat();
			break;
		default:
			npcCombat = new DefaultNpcCombat(characterName, character, "Sword");
			break;
		}
	}
	
	public virtual void runScript(int currentDirection) {
		switch(currentAction) {
		case "attack":
			npcCombat.attacking(currentDirection);
			break;
		case "defend":
			break;
		case "counter":
			break;
		default:
			break;
		}
			
		npcCombat.updateNpcCombat(currentDirection);
	}

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

	public string CurrentAction {
		get {return currentAction;}
		set {currentAction = value;}
	}
}

