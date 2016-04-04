using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcCombatUI : MonoBehaviour
{
	// UI bars
	private GameObject healthbar;
	private GameObject manabar;

	private float characterHeight;
	private GameObject characterParent;
	private GameObject combatUI;

	// stats for bars length
	private float startingHealth;
	private float startingMana;

	public NpcCombatUI(GameObject characterParent, float startingHealth, float startingMana) {
		this.startingHealth = startingHealth;
		this.startingMana = startingMana;
		this.characterParent = characterParent;

		combatUI = Instantiate(Resources.Load("UI/Combat UI")) as GameObject;
		characterHeight = this.characterParent.GetComponent<BoxCollider2D> ().bounds.extents.y * 1.5f;
		combatUI.transform.position = new Vector3(characterParent.transform.position.x, 
													characterParent.transform.position.y + characterHeight);

		healthbar = combatUI.transform.Find ("HealthMask/Health").gameObject;
		manabar = combatUI.transform.Find ("ManaMask/Mana").gameObject;
	}

	public void updateUI(float currentHealth, float currentMana) {
		healthbar.transform.localScale = new Vector3 (currentHealth / startingHealth, 1);
		manabar.transform.localScale = new Vector3 (currentMana / startingMana, 1);
		combatUI.transform.position = new Vector3(characterParent.transform.position.x, 
													characterParent.transform.position.y + characterHeight);
	}
}

