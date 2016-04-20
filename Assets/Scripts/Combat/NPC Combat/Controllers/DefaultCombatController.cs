using UnityEngine;
using System.Collections;

/**
 * Default combat reactions for an npc
 */
public class DefaultCombatController : MonoBehaviour
{
	// sets the class, takes the character name, and what action is currently happening
	protected DefaultNpcCombat npcCombat;
	protected string characterName;
	protected string currentAction = "";
	
	// Use this for initialization
	public DefaultCombatController (string characterName, GameObject character) {
		this.characterName = characterName;

		// creates a new version of combat depending on the character
		switch(characterName) {
		case "Minion":
			npcCombat = new DefaultNpcCombat(characterName, character, "Starter Sword"); //MinionCombat();
			break;
		default:
			npcCombat = new DefaultNpcCombat(characterName, character, "Starter Sword");
			break;
		}
	}

	// runs through the current action and responds accordingly
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

	// applies damage when hitting something with an attack
	public void respondToCollision(Collision2D col) {
		if(npcCombat.InAttack && col.contacts[0].otherCollider.name == characterName + " Attack") {
			npcCombat.applyAttackDamage(col.contacts [0].collider.gameObject);
			//((NpcCombat)npcCombat).applyAttackDamage (col.contacts [0].collider.gameObject);
		}
	}

	// checks health of the character or sets it
	public int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return npcCombat.Health;
		} else {
			npcCombat.Health = newHealthValue;
			return -1;
		}
	}

	// applies attack damage from the npc
	public void applyAiAttackDamage(GameObject targetCharacter) {
		npcCombat.applyAttackDamage(targetCharacter);
	}

	// the current action the npc is doing
	public string CurrentAction {
		get {return currentAction;}
		set {currentAction = value;}
	}
}

