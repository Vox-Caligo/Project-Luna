using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	protected GameObject character;
	protected string characterName;
	protected string characterWeapon;
	protected int health;
	protected int damage;
	protected int defense;

	// attack timer
	protected bool inAttack = false;
	public float timerTick = 1.5f;
	protected float maxTimer = 1.5f;
	protected bool inAttackDelay = false;
	protected float attackDelay = 0f;
	
	// attack box variables
	protected GameObject attackArea;
	protected float attackRange = 2;
	protected float attackWidth = 1;
	protected bool longRange = false;
	protected bool isHorizontal = false;

	public Combat(string characterName, GameObject character, string characterWeapon) {
		this.characterName = characterName;
		this.character = character;
		this.characterWeapon = characterWeapon;
		initiateAttackBox ();
		health = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Health");
		defense = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Defense");
		damage = (int)(GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue (this.characterWeapon, "Damage"));
		attackDelay = (int)(GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue (this.characterWeapon, "Speed"));
		attackWidth = GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue(characterWeapon, "Width");
		attackRange = GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue(characterWeapon, "Length");
	}

	public void attacking(int currentDirection) {
		if (!inAttack) {
			inAttack = true;
			launchAttack (currentDirection);
		}
	}

	// makes the attack area that the character will send out
	protected virtual void initiateAttackBox() {
		attackArea = new GameObject();
		attackArea.transform.parent = character.transform;
		attackArea.transform.position = attackArea.transform.parent.position;
		attackArea.name = characterName + " Attack";
	}
	
	// used for generating the appropriate attack hit box (size, direction, height, width)
	protected void launchAttack(int currentDirection) {		
		BoxCollider2D weaponHitbox = attackArea.AddComponent<BoxCollider2D>();
		weaponHitbox.size = new Vector2(attackWidth, attackRange);

		if(currentDirection <= 1 && !isHorizontal) {
			isHorizontal = true;
			attackArea.transform.Rotate(new Vector3(0,0,90));
		} else if (currentDirection > 1 && isHorizontal) {
			isHorizontal = false;
			attackArea.transform.Rotate(new Vector3(0,0,-90));
		}

		if(currentDirection == 0) { 		
			weaponHitbox.offset = new Vector2(0, attackRange); 
		} else if(currentDirection == 1) {	
			weaponHitbox.offset = new Vector2(0, -attackRange);
		} else if(currentDirection == 2) { 	
			weaponHitbox.offset = new Vector2(0, attackRange);
		} else if(currentDirection == 3) {	
			weaponHitbox.offset = new Vector2(0, -attackRange);
		}
	}	

	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	public void applyAttackDamage(GameObject target) {
		if(target.GetComponent<MasterBehavior>() != null) {
			print("Hitting " + target.name + " with health " + target.GetComponent<MasterBehavior>().characterHealth());
			target.GetComponent<MasterBehavior>().characterHealth(target.GetComponent<MasterBehavior>().characterHealth() - damage);

			if(target.GetComponent<MasterBehavior>().characterHealth() <= 0) {
				if(target.tag == "Player") {
					print("The Player has died!");
				} else {
					Destroy(target);
				}
			}
		}
		//endAttack();
	}

	protected void OnTriggerEnter2D(Collider2D other) {
		print("test");
	}

	protected virtual void FixedUpdate() {
		// have an attack delay after swings
		// end attack when opponent is hit
		if (!inAttackDelay) {
			if(inAttack) {
				if(timerCountdownIsZero()) {
					endAttack();
				}
			} 
		} else {
			if(timerCountdownIsZero()) {
				inAttack = false;
				inAttackDelay = false;
				timerTick = maxTimer;
			}
		}
	}

	protected void endAttack() {
		inAttackDelay = true;
		timerTick = attackDelay;
		Destroy(character.transform.FindChild(characterName + " Attack").GetComponent<BoxCollider2D>());
	}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
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
}
