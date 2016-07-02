using UnityEngine;
using System.Collections;

/**
 * The base class for all combat behaviors (includes NPCs and the player)
 */
public class Combat : MonoBehaviour {
	// properties of the character
	protected GameObject character;
	protected string characterName;
	protected string characterWeapon;
	protected int health;
	protected int maxHealth;
	protected int mana;
	protected int maxMana;
	protected int damage;
	protected int defense;
	protected int karma;
	protected int experiencePoints;

	// attack timer
	protected bool inAttack = false;
	protected bool inAttackDelay = false;
	protected float attackDelay = 0f;
	
	// attack box variables
	protected AttackArea attackArea;
	protected float attackWidth;
	protected float attackRange;
	protected bool longRange = false;
	protected float characterWidth;
	protected float characterHeight;

	protected bool manaRegeneration = false;
	protected bool healthRegeneration = false;

	// timer to tell if the player has been out of combat long enough to regenerate health
	protected bool inCombat = false;

	// timers for combat
	protected UtilTimer attackTimer;		// attacking
	protected UtilTimer attackDelayTimer;	// delay between attacks
	protected UtilTimer combatCooldownTimer;// delay after combat
	protected UtilTimer regenerationTimer;	// time to regenerate

	// databases
	protected StatDB statDatabase;
	protected WeaponDB weaponDatabase;

    // sounds
    protected SoundInterpreter sounds;
    protected string attackSound;

    // sets the base combat for all game characters
    public Combat(string characterName, GameObject character, string characterWeapon) {
		// sets the character and their weapon
		this.characterName = characterName;
		this.character = character;
		this.characterWeapon = characterWeapon;

		statDatabase = GameObject.Find ("Databases").GetComponent<StatDB> ();
		weaponDatabase = GameObject.Find ("Databases").GetComponent<WeaponDB> ();

		// sets character health
		health = statDatabase.getValue (this.characterName, "Health");
		maxHealth = health;

		// sets character mana
		mana = statDatabase.getValue (this.characterName, "Mana");
		maxMana = mana;

		// sets character defense
		defense = statDatabase.getValue (this.characterName, "Defense");

		// sets character damage
		damage = (int)(weaponDatabase.getValue (this.characterWeapon, "Damage"));

		// sets character karma
		karma = (int)(statDatabase.getValue (this.characterName, "Karma"));

        // sets character sounds
        sounds = new SoundInterpreter(this.character);

        //experiencePoints = (int)(statDatabase.getValue (this.characterName, "Experience"));

        // set the delay between character attacks
        attackDelay = (int)(weaponDatabase.getValue (this.characterWeapon, "Speed"));

		// set the area of the attack area
		attackWidth = weaponDatabase.getValue(characterWeapon, "Width");
		attackRange = weaponDatabase.getValue(characterWeapon, "Length");
		attackArea = new AttackArea (this.character, characterName, attackWidth, attackRange);

        // get the attack sound
        attackSound = weaponDatabase.getWeaponSound(characterWeapon);

        // sets the character's bounding box
        characterWidth = this.character.GetComponent<BoxCollider2D> ().bounds.extents.x * 2;
		characterHeight = this.character.GetComponent<BoxCollider2D> ().bounds.extents.y * 2;

		// sets the timers to be used
		attackTimer = new UtilTimer(1.5f, 1.5f);
		attackDelayTimer = new UtilTimer(attackDelay, attackDelay);	// use attackDelay = (int)(weaponDatabase.getValue (this.characterWeapon, "Speed")); 
		combatCooldownTimer = new UtilTimer(1.5f, 1.5f);
		regenerationTimer = new UtilTimer(1.5f, 1.5f);
	}

	// called when the player starts an attack
	public void attacking(int currentDirection) {
		inCombat = true;

		// makes sure the player can attack
		if (!inAttack && !inAttackDelay) {
			inAttack = true;
			attackArea.manipulateAttackArea(true, currentDirection); // used for generating the appropriate attack hit box (size, direction, height, width)
            sounds.playSound(weaponDatabase.getWeaponSound(characterWeapon), true);
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

	// updates what  is currently happening in combat
	protected void updateCombat(int currentDirection) {
		// end attack when opponent is hit

		if (inAttack) {
			// if the player is attack is finished, sets attack delay
			if (!attackTimer.runningTimerCountdown ()) {
				inAttack = false;
				inAttackDelay = true;
				attackArea.manipulateAttackArea(false);
			}
		} else if(inAttackDelay) {
			// when in delay,checks if it ends and allows attack again
			if (!attackDelayTimer.runningTimerCountdown ()) {
				inAttackDelay = false;
			}
		}
			
		if(inCombat) {
			// checks that the player has been out of combat long enough
			if(!combatCooldownTimer.runningTimerCountdown()) {
				inCombat = false;
			}
		} else {
			// when the regeneration timer hits, regenerate
			if(!regenerationTimer.runningTimerCountdown()) {
				regeneration();
			}
		}
	}

	// restore mana/health if below the thresholds
	protected void regeneration() {
		if(manaRegeneration && mana < maxMana) {
			mana++;
		}

		// goes until the character is full/half health (depends on current health)
		if(healthRegeneration && (health < maxHealth / 2 || (health > maxHealth / 2 && health < maxHealth))) {
			health++;
		}
	}

	// change the weapon that the character is using
	public void changeWeapon(string newWeapon) {
		if (this.characterWeapon != newWeapon) {
			this.characterWeapon = newWeapon;

			damage = (int)weaponDatabase.getValue (this.characterWeapon, "Damage");
			attackDelay = weaponDatabase.getValue (this.characterWeapon, "Speed");
			attackRange = weaponDatabase.getValue (this.characterWeapon, "Length");
			attackWidth = weaponDatabase.getValue (this.characterWeapon, "Width");
			attackDelayTimer.RunningTimerMax = attackDelay;

			attackArea.brandNewAttackArea (attackWidth, attackRange);
		}
	}

	// checks if the character is attacking
	public bool InAttack {
		get {	return inAttack; }
	}

	// get/set the character's health
	public int Health {	
		get {	return health;	} 
		set {	health = value;	}
	}

	// get/set the character's max health
	public int MaxHealth {
		get {	return maxHealth;	} 
		set {	maxHealth = value;	}
	}

	// get/set the character's mana
	public int Mana {	
		get {	return mana;	} 
		set {	mana = value;	}
	}

	// get/set the character's max mana
	public int MaxMana {
		get {	return maxMana;	} 
		set {	maxMana = value;	}
	}

	// get/set the character's damage
	public int Damage {	
		get {	return damage;	} 
		set {	damage = value;	}
	}

	// get/set the character's defense
	public int Defense {	
		get {	return defense;	} 
		set {	defense = value;}
	}

	// get/set the character's delay time between attacks
	public float AttackDelay {
		get {	return attackDelay;	} 
		set {	attackDelay = value;}
	}

	// get/set the character's weapon width
	public float AttackWidth {
		get {	return attackWidth;	} 
		set {	attackWidth = value;}
	}

	// get/set the character's weapon range
	public float AttackRange {
		get {	return attackRange;	} 
		set {	attackRange = value;}
	}

	// get/set if the character is in combat
	public bool InCombat {
		get { return inCombat; }
		set { inCombat = value; }
	}

	// get/set if mana can regenerate
	public bool ManaRegeneration {
		get { return manaRegeneration; }
		set { manaRegeneration = value; }
	}

	// get/set if health can regenerate
	public bool HealthRegeneration {
		get { return healthRegeneration; }
		set { healthRegeneration = value; }
	}
}
