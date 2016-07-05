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
    protected Health health;
    protected Mana mana;
	protected int damage;
	protected int defense;
	protected int karma;
	protected int experiencePoints;

	// attack timer
	protected bool inAttack = false;
	protected bool inAttackDelay = false;
	protected float attackDelay = 0f;
	
	// attack box variables
	protected float attackWidth;
	protected float attackRange;
	protected bool longRange = false;
	protected float characterWidth;
	protected float characterHeight;

	// timer to tell if the player has been out of combat long enough to regenerate health
	protected bool inCombat = false;

	// timers for combat
	protected UtilTimer attackTimer;		// attacking
	protected UtilTimer attackDelayTimer;	// delay between attacks
	protected UtilTimer combatCooldownTimer;// delay after combat

	// databases
	protected StatDB statDatabase;
	protected WeaponDB weaponDatabase;

    // sets the base combat for all game characters
    public Combat(string characterName, GameObject character, string characterWeapon) {
		// sets the character and their weapon
		this.characterName = characterName;
		this.character = character;
		this.characterWeapon = characterWeapon;

		statDatabase = GameObject.Find ("Databases").GetComponent<StatDB> ();
		weaponDatabase = GameObject.Find ("Databases").GetComponent<WeaponDB> ();

		// sets character health
		health = new Health(statDatabase.getValue (this.characterName, "Health"));

		// sets character mana
		mana = new Mana(statDatabase.getValue (this.characterName, "Mana"));

		// sets character defense
		defense = statDatabase.getValue (this.characterName, "Defense");

		// sets character karma
		karma = (int)(statDatabase.getValue (this.characterName, "Karma"));

        //experiencePoints = (int)(statDatabase.getValue (this.characterName, "Experience"));

        // set the delay between character attacks
        attackDelay = weaponDatabase.getValue (this.characterWeapon, "Speed");

        // sets the character's bounding box
        characterWidth = this.character.GetComponent<BoxCollider2D> ().bounds.extents.x * 2;
		characterHeight = this.character.GetComponent<BoxCollider2D> ().bounds.extents.y * 2;

		// sets the timers to be used
		attackTimer = new UtilTimer(attackDelay, attackDelay);
		attackDelayTimer = new UtilTimer(attackDelay, attackDelay);
		combatCooldownTimer = new UtilTimer(attackDelay, attackDelay);
	}

	// called when the player starts an attack
	public void attacking(int currentDirection) {
		inCombat = true;

		// makes sure the player can attack
		if (!inAttack && !inAttackDelay) {
			inAttack = true;
			AttackArea attackArea = new AttackArea(this.character, this, weaponDatabase.getWeapon(characterWeapon), currentDirection);
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
            // regenerate health and mana if not in combat
            health.regeneration();
            mana.regeneration();
		}
	}

	// change the weapon that the character is using
	public void changeWeapon(string newWeapon) {
		if (this.characterWeapon != newWeapon) {
			this.characterWeapon = newWeapon;
			attackDelayTimer.RunningTimerMax = attackDelay;
		}
	}

	// checks if the character is attacking
	public bool InAttack {
		get {	return inAttack; }
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

	// get/set if the character is in combat
	public bool InCombat {
		get { return inCombat; }
		set { inCombat = value; }
	}

    public Mana Mana {
        get { return mana; }
    }

    public Health Health {
        get { return health; }
    }
}
