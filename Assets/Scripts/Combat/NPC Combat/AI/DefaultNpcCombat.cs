using UnityEngine;
using System.Collections;

public class DefaultNpcCombat : Combat
{
	// UI above NPC characaters
	protected NpcCombatUI npcCombatUi;

	public DefaultNpcCombat(string characterName, GameObject character, string characterWeapon) : base(characterName, character, characterWeapon) {	
		npcCombatUi = new NpcCombatUI(character, health, mana);
	}

	public void updateNpcCombat(int currentDirection) {
		if (this.health <= 0) {
			npcCombatUi.destroyUi ();
			Destroy (character);
		} else {
			updateCombat (currentDirection);
			npcCombatUi.updateUI(health, mana);
		}
	}
}

