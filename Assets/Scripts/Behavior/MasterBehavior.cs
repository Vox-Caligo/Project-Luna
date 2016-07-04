using UnityEngine;
using System.Collections;

/**
 * The parent class of all character behavior, both player and NPC.
 * Has a base set of classes that are inherited by the children.
 */
public class MasterBehavior : MonoBehaviour
{
	protected string characterName;								// the character
	protected Combat characterCombat;							// combat AI
	protected CutsceneController cutsceneController;			// cutscene behavior
	protected CharacterMovementController characterMovement;	// movement AI
	protected DefaultMovementController npcMovement;			// movement controller
	protected DefaultCombatController npcCombat;				// combat controller
	protected int currentDirection = 0;							// the current facing direction
	protected int karma;										// karma the character has

	// know when/where to move
	// know when to attack

	// Use this for initialization
	protected virtual void Start() {
		cutsceneController = GameObject.Find ("Databases").GetComponent<CutsceneController> ();
	}

	// virtual for collision enters
	protected virtual void OnCollisionEnter2D (Collision2D col) { }

	// changing values but done so combat doesn't have to be seen by everyone.
	public virtual int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return characterCombat.Health.CurrentHealth;
		} else {
			characterCombat.Health.CurrentHealth = newHealthValue;
			return -1;
		}
	}

	// setting combatants to inAttack
	public virtual void characterInCombat() {
		//characterCombat.setInCombat();
	}

	// get/set for the karma value on a character
	public int Karma {
		get { return karma; }
		set { karma = value; }
	}
}