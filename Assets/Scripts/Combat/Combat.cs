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
	public float timerTick = 1.5f;
	protected float maxTimer = 1.5f;
	protected bool inAttackDelay = false;
	protected float attackDelay = 0f;
	
	// attack box variables
	protected AttackArea attackArea;
	protected bool longRange = false;
	protected float characterWidth;
	protected float characterHeight;

	// timer to tell if the player has been out of combat long enough to regenerate health
	protected bool inCombat = false;
	public float inCombatTimerTick = 1.5f;
	protected float inCombatMaxTimer = 1.5f;

	// timer to tell if the player has been out of combat long enough to regenerate health
	public float regenerationTimerTick = 1.5f;
	protected float regenerationMaxTimer = 1.5f;

	// UI above NPC characaters
	protected NpcCombatUI npcCombatUi;

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
		attackArea.resizeHitbox(true);

		npcCombatUi = new NpcCombatUI(this.character, health, mana);
	}

	public void attacking(int currentDirection) {
		setInCombat();

		if (!inAttack) {
			inAttack = true;
			launchAttack (currentDirection);
		}
	}
	
	// used for generating the appropriate attack hit box (size, direction, height, width)
	protected void launchAttack(int currentDirection) {
		setInCombat();
		attackArea.resizeHitbox(false, currentDirection);
	}	

	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	public void applyAttackDamage(GameObject target) {
		if(target.GetComponent<MasterBehavior>() != null) {
			print("Hitting " + target.name + " with health " + target.GetComponent<MasterBehavior>().characterHealth());
			target.GetComponent<MasterBehavior>().characterHealth(target.GetComponent<MasterBehavior>().characterHealth() - damage);
			target.GetComponent<MasterBehavior>().characterInCombat();

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

		if(inCombat) {
			inCombatTimerCountdown();
		} else {
			regenerationTimer();
		}

		attackArea.rearrangeCollisionArea(currentDirection);
		npcCombatUi.updateUI(health, mana);
	}

	protected void endAttack() {
		inAttackDelay = true;
		timerTick = attackDelay;
		attackArea.resizeHitbox(true);
		character.transform.FindChild(characterName + " Attack").GetComponent<BoxCollider2D>().isTrigger = true;
	}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}

	// sets the timer for combat so regeneration cannot occur until it is zero
	public void setInCombat() {
		inCombat = true;
		inCombatTimerTick = inCombatMaxTimer;
	}

	protected void regenerationTimer() {
		if(regenerationTimerTick > 0) {
			regenerationTimerTick -= Time.deltaTime;
		} else {
			regeneration();
			regenerationTimerTick = regenerationMaxTimer;
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

	protected void inCombatTimerCountdown() {
		if(inCombatTimerTick > 0) {
			inCombatTimerTick -= Time.deltaTime;
		} else {
			inCombat = false;
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

	public bool InCombat {
		get { return inCombat; }
		set { inCombat = value; }
	}
}
