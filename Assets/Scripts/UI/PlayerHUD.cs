using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHUD : MonoBehaviour {
	// hud components
	private GameObject standardHud;
	private GameObject healthbarMask;
	private GameObject healthbar;
	private GameObject manabarMask;
	private GameObject manabar;
	private Image playerProfile;

	private int maxHealth;
	private int maxMana;
	private int currentHealth;
	private int currentMana;

	public PlayerHUD(string currentProtagonist, int maxHealth, int maxMana) {
		this.maxHealth = maxHealth;
		this.maxMana = maxMana;
		currentHealth = maxHealth;
		currentMana = maxMana;

		standardHud = Instantiate(Resources.Load("UI/PlayerHud")) as GameObject;

		healthbarMask = standardHud.transform.Find ("HealthMask").gameObject;
		healthbar = standardHud.transform.Find ("HealthMask/Health").gameObject;

		manabarMask = standardHud.transform.Find ("ManaMask").gameObject;
		manabar = standardHud.transform.Find ("ManaMask/Mana").gameObject;

		float healthbarSize = (maxHealth * .2f) / 20f;
		healthbarMask.transform.localScale = new Vector3 (healthbarSize, 1); ;
		healthbar.transform.localScale = new Vector3 (healthbarSize, 1); 

		float manabarSize = 0;
		if(maxMana > 0) {
			manabarSize = 1 - (maxMana * .05f);
		} 

		manabarMask.transform.localScale = new Vector3 (manabarSize, 1); 
		manabar.transform.localScale = new Vector3 (manabarSize, 1); 

		playerProfile = standardHud.transform.Find("CharacterProfile").GetComponent<Image>();
		playerProfile.sprite = Resources.Load (GameObject.Find ("Databases").GetComponent<SpeakerDB> ().getProfile (currentProtagonist), typeof(Sprite)) as Sprite;
	}

	// updates the hud with the current amount of mana and health
	public void hudUpdate(int currentHealth, int currentMana) {
		if(this.currentHealth != currentHealth) {
			this.currentHealth = currentHealth;

			if(currentHealth > 0) {
				healthbar.transform.localScale = new Vector3 ((float)currentHealth / maxHealth, 1); 
			} else {
				healthbar.transform.localScale = new Vector3 (0, 1); 
			}
		}

		if(this.currentMana != currentMana) {
			this.currentMana = currentMana; 

			if(currentMana > 0) {
				manabar.transform.localScale = new Vector3 ((float)currentMana / maxMana, 1);
			} else {
				manabar.transform.localScale = new Vector3 (0, 1); 
			}
		}
	}

	// if the player gets more health or mana during gameplay
	public void increaseStats(int newMaxHealth, int newMaxMana) {
		maxHealth = newMaxHealth;
		maxMana = newMaxMana;
	}
}