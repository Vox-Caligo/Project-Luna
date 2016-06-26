using UnityEngine;
using System.Collections;

/**
 * A version of combat specific for npcs
 */
public class DefaultNpcCombat : Combat
{
	// UI above NPC characaters
	protected NpcCombatUI npcCombatUi;

	// creates a new combat for this npc
	public DefaultNpcCombat(string characterName, GameObject character, string characterWeapon) : base(characterName, character, characterWeapon) {	
		npcCombatUi = new NpcCombatUI(character, health, mana);
	}

	// updates the ai of the attacking npc
	public void updateNpcCombat(int currentDirection) {
		if (this.health <= 0) {
			// removes the npc if they die and applies karma to the player
			PlayerCombat player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster> ().currentCharacterCombat ();
			player.Karma = player.Karma + this.karma;
			npcCombatUi.destroyUi ();

            KillQuestComponent questComponent = character.GetComponent<KillQuestComponent>();
            if (questComponent != null) {
                questComponent.updateKillQuest();
            }

			Destroy (character);
		} else {
			// goes through attack ai and does what is needed
			updateCombat (currentDirection);
			npcCombatUi.updateUI(health, mana);
		}
	}
}

