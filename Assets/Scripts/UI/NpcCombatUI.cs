using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Creates a bar showing both health and mana that an npc
 * currently has. This bar floats above the npc and reacts
 * to changes in either value. When needed, it can be easily
 * removed from the game (ie death).
 */
public class NpcCombatUI : MonoBehaviour
{
	// UI bars
	private GameObject healthbar;
	private GameObject manabar;

	// used for placement of UI bars
	private float characterHeight;
	private GameObject characterParent;
	private GameObject combatUI;

	// stats for bars length
	private float startingHealth;
	private float startingMana;

	// constructs a default ui for an npc
	public NpcCombatUI(GameObject characterParent, float startingHealth, float startingMana) {
		// sets the starting values
		this.startingHealth = startingHealth;
		this.startingMana = startingMana;
		this.characterParent = characterParent;

		// creates a new npc ui and attaches it to the npc
		combatUI = Instantiate(Resources.Load("UI/Combat UI")) as GameObject;
		characterHeight = this.characterParent.GetComponent<BoxCollider2D> ().bounds.extents.y * 1.5f;
		combatUI.transform.position = new Vector3(characterParent.transform.position.x, 
													characterParent.transform.position.y + characterHeight);

		// finds the health/mana bars
		healthbar = combatUI.transform.Find ("HealthMask/Health").gameObject;
		manabar = combatUI.transform.Find ("ManaMask/Mana").gameObject;
	}

	// updates the ui to show the current amount of health and mana
	public void updateUI(float currentHealth, float currentMana) {
		healthbar.transform.localScale = new Vector3 (currentHealth / startingHealth, 1);
		manabar.transform.localScale = new Vector3 (currentMana / startingMana, 1);
		combatUI.transform.position = new Vector3(characterParent.transform.position.x, 
													characterParent.transform.position.y + characterHeight);
	}

	// removes the UI from above the npc
	public void destroyUi() {
		Destroy (combatUI);
	}
}