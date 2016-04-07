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

		Vector2 healthbarSize = new Vector2(maxHealth * 1.5f, healthbar.GetComponent<RectTransform>().sizeDelta.y);
		healthbarMask.GetComponent<RectTransform>().sizeDelta = healthbarSize;
		healthbar.GetComponent<RectTransform>().sizeDelta = healthbarSize;

		Vector2 manabarSize = new Vector2(maxMana * 1.5f, healthbar.GetComponent<RectTransform>().sizeDelta.y);
		manabarMask.GetComponent<RectTransform>().sizeDelta = manabarSize;
		manabar.GetComponent<RectTransform>().sizeDelta = manabarSize;

		playerProfile = standardHud.transform.Find("CharacterProfile").GetComponent<Image>();
		playerProfile.sprite = Resources.Load (GameObject.Find ("Databases").GetComponent<SpeakerDB> ().getProfile (currentProtagonist), typeof(Sprite)) as Sprite;
		print("CH: " + maxHealth);
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