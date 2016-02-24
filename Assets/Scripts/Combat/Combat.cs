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
		initiateAttackBox ();
		health = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Health");
		damage = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Damage");
		defense = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Defense");
	}

	protected void attacking(int currDirection) {
		if (!inAttack) {
			inAttack = true;
			launchAttack (currDirection, attackWidth, attackRange);
		}
	}

	// makes the attack area that the character will send out
	protected virtual void initiateAttackBox() {
		attackArea = new GameObject();
		attackArea.transform.parent = character.transform;
		attackArea.transform.position = new Vector3(character.transform.position.x, character.transform.position.y);
		attackArea.name = characterName + " Attack";
	}
	
	// used for generating the appropriate attack hit box (size, direction, height, width)
	protected void launchAttack(int currDirection, float atkWidth, float atkLength) {		
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

		if((currDirection <= 1 && !isHorz) || (currDirection > 1 && isHorz)) {		// left and  right
			isHorz = !isHorz;
			attackArea.transform.Rotate(new Vector3(0,0,90));
		}
	}	

	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	public void applyAttackDamage(GameObject target) {
		if(target.GetComponent<MasterBehavior>() != null) {
			print("Hitting " + target.name + " with health " + target.GetComponent<MasterBehavior>().characterHealth());
			target.GetComponent<MasterBehavior>().characterHealth(target.GetComponent<MasterBehavior>().characterHealth() - damage);

			if(target.GetComponent<MasterBehavior>().characterHealth() <= 0) {
				Destroy(target);
			}
		}
	}

	protected void OnTriggerEnter2D(Collider2D other) {
		print("test");
	}

	protected virtual void FixedUpdate() {
		if(inAttack) {
			if(timerTick > 0) {
				timerTick -= Time.deltaTime;						
			} else if (timerTick <= 0 /*weapon speed*/) {
				inAttack = false;
				timerTick = maxTimer;
				Destroy(character.transform.FindChild(characterName + " Attack").GetComponent<BoxCollider2D>());
			}
		}
	}

	public bool InAttack {
		get {	return inAttack; }
	}

	public int Health {	
		get {	return health;	} 
		set {	health = value;	}
	}

	public int Damage {	
		get {	return damage;	} 
		set {	damage = value;	}
	}

	public int Defense {	
		get {	return defense;	} 
		set {	defense = value;}
	}

	public virtual void updatePlayerCombat() {}
}
