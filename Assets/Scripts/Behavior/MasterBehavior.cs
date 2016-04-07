using UnityEngine;
using System.Collections;

public class MasterBehavior : MonoBehaviour
{
	protected string characterName;
	protected Combat characterCombat;
	protected CutsceneController cutsceneController;
	protected CharacterMovementController characterMovement;
	protected DefaultMovementController npcMovement;
	protected DefaultCombatController npcCombat;
	protected int currentDirection = 0;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	protected virtual void Start() {
		cutsceneController = GameObject.Find ("Databases").GetComponent<CutsceneController> ();
	}

	protected virtual void OnCollisionEnter2D (Collision2D col) { }

	// changing values but done so combat doesn't have to be seen by everyone.
	public virtual int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return characterCombat.Health;
		} else {
			characterCombat.Health = newHealthValue;
			return -1;
		}
	}

	// setting combatants to inAttack
	public virtual void characterInCombat() {
		//characterCombat.setInCombat();
	}
}