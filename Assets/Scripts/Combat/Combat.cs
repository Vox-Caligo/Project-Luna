using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	protected GameObject character;
	protected string characterName;
	protected int health;
	protected int damage;
	protected int defense;

	// attack timer
	protected bool inAttack = false;
	public float timerTick = 1.5f;
	protected float maxTimer = 1.5f;
	
	// attack box variables
	protected GameObject attackArea;
	protected float attackRange = 2;
	protected float attackWidth = 1;
	protected bool longRange = false;
	protected bool isHorz = false;

	public Combat(string characterName, GameObject character) {
		this.characterName = characterName;
		this.character = character;
		health = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Health");
		damage = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Damage");
		defense = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Defense");
	}

	protected void attacking(int currDirection) {
		if (!inAttack) {
			inAttack = true;
			if(GameObject.Find ("Player Attack") == null /* || changed weapon*/)
				initiateAttack ();
			changeAttackPosition (currDirection, attackWidth, attackRange);
		}
	}

	// makes the attack area that the character will send out
	protected virtual void initiateAttack() {
		attackArea = new GameObject();
		attackArea.transform.parent = character.transform;
		attackArea.transform.position = new Vector3(character.transform.position.x, character.transform.position.y);
	}
	
	// used for generating the appropriate attack hit box (size, direction, height, width)
	protected void changeAttackPosition(int currDirection, float atkWidth, float atkLength) {		
		if(currDirection == 0) { attackArea.transform.position = new Vector3(character.transform.position.x - 1, character.transform.position.y); } 
		else if(currDirection == 1) {	attackArea.transform.position = new Vector3(character.transform.position.x + 1, character.transform.position.y);} 
		else if(currDirection == 2) { attackArea.transform.position = new Vector3(character.transform.position.x, character.transform.position.y + 1);} 
		else if(currDirection == 3) {	attackArea.transform.position = new Vector3(character.transform.position.x, character.transform.position.y - 1);}
		
		if(atkWidth < .5f) 
			atkWidth = .5f;
		if(atkLength < .5f) 
			atkLength = .5f;
		
		BoxCollider2D weaponHitbox = attackArea.AddComponent<BoxCollider2D>();
		weaponHitbox.size = new Vector2(atkWidth, atkLength);
		weaponHitbox.isTrigger = true;
		weaponHitbox.name = "Player Attack";

		if((currDirection <= 1 && !isHorz) || (currDirection > 1 && isHorz)) {		// left and  right
			isHorz = !isHorz;
			attackArea.transform.Rotate(new Vector3(0,0,90));
		}
	}	

	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	protected void applyAttackDamage(string targetName) {
		//int atkDamage = damage - DialogueLua.GetActorField(targetName, "Defense").AsInt;
		//DialogueLua.SetActorField(targetName, "Health", DialogueLua.GetActorField(targetName, "Health").AsInt - atkDamage);
	}

	protected virtual void FixedUpdate() {
		if(inAttack) {
			if(timerTick > 0) {
				timerTick -= Time.deltaTime;						
			} else if (timerTick <= 0 /*weapon speed*/) {
				inAttack = false;
				timerTick = maxTimer;
				Destroy(GameObject.Find ("Player Attack").GetComponent<BoxCollider2D>());
			}
		}
	}
}
