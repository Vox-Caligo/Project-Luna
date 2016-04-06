using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	// hud components
	private GameObject standardHud;
	private GameObject healthbar;
	private GameObject manabar;

	private int startingHealth;
	private int startingMana;
	private int currentHealth;
	private int currentMana;

	// updates the hud with the current amount of mana and health
	public void hudUpdate(int currentHealth, int currentMana) {
		if(standardHud == null) {
			this.currentHealth = currentHealth;
			this.currentMana = currentMana;

			startingHealth = currentHealth;
			startingMana = currentMana;

			standardHud = Instantiate(Resources.Load("UI/PlayerHud")) as GameObject;
			healthbar = standardHud.transform.Find ("Health").gameObject;
			manabar = standardHud.transform.Find ("Mana").gameObject;
		}

		if(this.currentHealth != currentHealth) { 
			this.currentHealth = currentHealth;
			float healthScaler = (float)currentHealth / startingHealth;

			//healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2(healthbar.GetComponent<RectTransform>().rect.width, healthbar.GetComponent<RectTransform>().rect.height);
			//healthbar.transform.localScale = new Vector3(healthScaler, healthbar.transform.localScale.y);
			healthbar.transform.localScale = new Vector3 ((float)currentHealth / startingHealth, 1);
		}

		if(this.currentMana != currentMana) { 
			manabar.transform.localScale = new Vector3 ((float)currentMana / startingMana, 1);
		}
	}
}