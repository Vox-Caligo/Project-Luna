using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NpcCombatUI : MonoBehaviour
{
	// UI bars


	// stats for bars length
	private float startingHealth;
	private float startingMana;

	public NpcCombatUI(GameObject parentObject, float startingHealth, float startingMana) {
		this.startingHealth = startingHealth;
		this.startingMana = startingMana;

		GameObject combatUI = Instantiate(Resources.Load("UI/CombatUI")) as GameObject;
		combatUI.transform.parent = parentObject.transform;
	}

	public void updateUI(float currentHealth, float currentMana) {

	}
}

