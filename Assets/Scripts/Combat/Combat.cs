using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {
	protected GameObject character;
	protected string characterName;
	protected string characterWeapon;
	protected int health;
	protected int maxHealth;
	protected int mana;
	protected int maxMana;
	protected int damage;
	protected int defense;

	// attack timer
	protected bool inAttack = false;
	protected bool inAttackDelay = false;
	protected float attackDelay = 0f;
	
	// attack box variables
	protected AttackArea attackArea;
	protected bool longRange = false;
	protected float characterWidth;
	protected float characterHeight;

	// timer to tell if the player has been out of combat long enough to regenerate health
	protected bool inCombat = false;

	protected UtilTimer attackTimer;
	protected UtilTimer attackDelayTimer;
	protected UtilTimer combatCooldownTimer;
	protected UtilTimer regenerationTimer;

	public Combat(string characterName, GameObject character, string characterWeapon) {
		this.characterName = characterName;
		this.character = character;
		this.characterWeapon = characterWeapon;

		health = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Health");
		maxHealth = health;
		mana = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Mana");
		maxMana = mana;
		defense = GameObject.Find ("Databases").GetComponent<StatDB> ().getValue (this.characterName, "Defense");
		damage = (int)(GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue (this.characterWeapon, "Damage"));
		attackDelay = (int)(GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue (this.characterWeapon, "Speed"));

		float attackWidth = GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue(characterWeapon, "Width");
		float attackRange = GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue(characterWeapon, "Length");
		attackArea = new AttackArea (this.character, characterName, attackWidth, attackRange);

		characterWidth = this.character.GetComponent<BoxCollider2D> ().bounds.extents.x * 2;
		characterHeight = this.character.GetComponent<BoxCollider2D> ().bounds.extents.y * 2;

		attackTimer = new UtilTimer(1.5f, 1.5f);
		attackDelayTimer = new UtilTimer(1.5f, 1.5f);	// use attackDelay = (int)(GameObject.Find ("Databases").GetComponent<WeaponDB> ().getValue (this.characterWeapon, "Speed")); 
		combatCooldownTimer = new UtilTimer(1.5f, 1.5f);
		regenerationTimer = new UtilTimer(1.5f, 1.5f);
	}

	public void attacking(int currentDirection) {
		inCombat = true;

		if (!inAttack && !inAttackDelay) {
			inAttack = true;
			attackArea.manipulateAttackArea(true, currentDirection); // used for generating the appropriate attack hit box (size, direction, height, width)
		}
	}

	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	public void applyAttackDamage(GameObject target) {
		if(target.GetComponent<MasterBehavior>() != null) {
			print("Hitting " + target.name + " with health " + target.GetComponent<MasterBehavior>().characterHealth());
			target.GetComponent<MasterBehavior>().characterHealth(target.GetComponent<MasterBehavior>().characterHealth() - damage);
			target.GetComponent<MasterBehavior>().characterInCombat();
		}
	}

	protected void updateCombat(int currentDirection) {
		// end attack when opponent is hit

		if (inAttack) {
			if (!attackTimer.runningTimerCountdown ()) {
				inAttack = false;
				inAttackDelay = true;
				attackArea.manipulateAttackArea(false);
			}
		} else if(inAttackDelay) {
			if (!attackDelayTimer.runningTimerCountdown ()) {
				inAttackDelay = false;
			}
		}

		if(inCombat) {
			if(!combatCooldownTimer.runningTimerCountdown()) {
				inCombat = false;
			}
		} else {
			if(!regenerationTimer.runningTimerCountdown()) {
				regeneration();
			}
		}
	}

	protected void regeneration() {
		if(mana < maxMana) {
			mana++;
		}

		if(health < maxHealth / 2 || (health > maxHealth / 2 && health < maxHealth)) {
			health++;
		}
	}

	public bool InAttack {
		get {	return inAttack; }
	}

	public int Health {	
		get {	return health;	} 
		set {	health = value;	}
	}

	public int Mana {	
		get {	return mana;	} 
		set {	mana = value;	}
	}

	public int Damage {	
		get {	return damage;	} 
		set {	damage = value;	}
	}

	public int Defense {	
		get {	return defense;	} 
		set {	defense = value;}
	}

	public bool InCombat {
		get { return inCombat; }
		set { inCombat = value; }
	}
}
