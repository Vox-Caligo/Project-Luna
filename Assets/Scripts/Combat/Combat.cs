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
	protected BoxCollider2D weaponHitbox;
	protected float attackRange = 2;
	protected float attackWidth = 1;
	protected bool longRange = false;
	protected bool isHorizontal = false;
	protected float characterWidth;
	protected float characterHeight;

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

		characterWidth = this.character.GetComponent<BoxCollider2D> ().bounds.extents.x * 2;
		characterHeight = this.character.GetComponent<BoxCollider2D> ().bounds.extents.y * 2;
		resizeHitbox(true);
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

		weaponHitbox = attackArea.AddComponent<BoxCollider2D>();
		weaponHitbox.isTrigger = true;
	}
	
	// used for generating the appropriate attack hit box (size, direction, height, width)
	protected void launchAttack(int currentDirection) {
		resizeHitbox(false, currentDirection, new Vector2(attackWidth, attackRange));
		weaponHitbox.isTrigger = false;
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
	}

	protected void updateCombat(int currentDirection) {
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

		rearrangeAttackHitbox(currentDirection);
	}

	protected void rearrangeAttackHitbox(int currentDirection) {
		if(currentDirection % 2 == 0 && !isHorizontal) {
			isHorizontal = true;
			attackArea.transform.Rotate(new Vector3(0,0,90));
		} else if (currentDirection % 2 == 1 && isHorizontal) {
			isHorizontal = false;
			attackArea.transform.Rotate(new Vector3(0,0,-90));
		}

		if(currentDirection == 0 || currentDirection == 1) { 		
			weaponHitbox.offset = new Vector2(0, attackRange); 
		} else {	
			weaponHitbox.offset = new Vector2(0, -attackRange);
		} 
	}

	protected void endAttack() {
		inAttackDelay = true;
		timerTick = attackDelay;
		resizeHitbox(true);
		character.transform.FindChild(characterName + " Attack").GetComponent<BoxCollider2D>().isTrigger = true;
		//Destroy(character.transform.FindChild(characterName + " Attack").GetComponent<BoxCollider2D>());
	}

	protected void resizeHitbox(bool reset, int currentDirection = 0, Vector2 newWeaponHitboxSize = new Vector2()) {
		if (reset) {
			if (currentDirection == 0 || currentDirection == 2) {
				weaponHitbox.size = new Vector2 (characterHeight, .06f);
			} else {
				weaponHitbox.size = new Vector2(.06f, characterWidth);
			} 

		} else {
			weaponHitbox.size = newWeaponHitboxSize;
		}
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
