using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AegisHUD : MonoBehaviour {
	// hud components
	private GameObject healthBar;
	private GameObject[] hearts;
	private GameObject[] potions;
	
	public bool isShown = false;
	/*
	// sets the starting health and mana that the player will have
	void Start() {
		setUp();
	}
	
	private void setUp() {
		hearts = new GameObject[DialogueLua.GetActorField("Player", "Max Health").AsInt];
		potions = new GameObject[DialogueLua.GetActorField("Player", "Max Mana").AsInt];
		healthBar = GameObject.Find ("Status Bar");
	}
	
	// updates the hud with the current amount of mana and health
	public void hudUpdate(int health, int mana) {
		// destroys the previous items
		if(hearts != null) {
			for(int i = hearts.Length - 1; i >= 0; i--) {
				Destroy(hearts[i]);
			}
			
			for(int i = potions.Length - 1; i >= 0; i--) {
				Destroy(potions[i]);
			}
		} else {
			setUp();
		}
		
		int hudRunner = 0;
		int healthHolder = health;
		int maxHealth = hearts.Length;
		
		// places the max amount of hearts and fills the amount the player currently has
		while(maxHealth > 0){
			GameObject heart = new GameObject();
			heart.AddComponent<Image>();
			
			if(healthHolder - 6 >= 0){
				healthHolder -= 6;
				maxHealth -= 6;
				heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Full Heart");
			} else if(healthHolder > 0) {
				switch(healthHolder){
				case 1:
					heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/First Heart");
					break;
				case 2:
					heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Second Heart");
					break;
				case 3:
					heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Third Heart");
					break;
				case 4:
					heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Fourth Heart");
					break;
				case 5:
					heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Fifth Heart");
					break;
				default:
					break;
				}
				
				healthHolder -= healthHolder;
				maxHealth -= 6;
			} else {
				maxHealth -= 6;
				heart.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Empty Heart");
			}
			
			heart.GetComponent<Image>().transform.localScale = new Vector3(.5f, .5f, 0);
			heart.transform.parent = healthBar.transform;
			heart.transform.position = new Vector3(65 + 50 * hudRunner, 735, 0);
			hearts[hudRunner] = heart;
			
			hudRunner++;
		}
		
		// places as much mana as the player has
		for(int i = 0; i < potions.Length; i++) {
			GameObject manaBottle = new GameObject();
			manaBottle.AddComponent<Image>();
			manaBottle.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("HUD/Mana");
			manaBottle.GetComponent<Image>().transform.localScale = new Vector3(.5f, .5f, 0);
			manaBottle.transform.parent = healthBar.transform;
			manaBottle.transform.position = new Vector3(65 + 50 * i, 675, 0);
			potions[i] = manaBottle;
		}
	}
	
	// changes the interactability and visibility
	public void changeVisibility(bool makeViewable) { 
		if(makeViewable) {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1;
			isShown = true;
		} else {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 0;
			isShown = false;
		}
	}
	*/
}