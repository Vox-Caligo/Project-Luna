using UnityEngine;
using System.Collections;

public class DefaultAI : MonoBehaviour
{
	public string characterName;
	protected bool hostile = false;
	protected Combat characterCombat;
	protected CharacterMovement characterMovement;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	public virtual void Start() {
		print ("Not going here I guess?");
		characterCombat = new NpcCombat(characterName, this.gameObject);
		characterMovement = new NpcMovement(characterName, this.gameObject);
	}
	
	protected virtual void processDecisions() {
		// determine if actor should move, attack, or other
		// if move, determine how to move
		// if attack, determine how to attack
		print("I am no minion!");
	}

	// Update is called once per frame
	protected virtual void FixedUpdate ()
	{
		processDecisions();
	}

	protected virtual void OnCollisionEnter2D (Collision2D col) {
		//characterMovement.npcMovement.respondToCollision (col);
		// do same for combat
	}

	// changing values but done so combat doesn't have to be seen by everyone.
	public int characterHealth(int newHealthValue = -1) {
		if (newHealthValue == -1) {
			return characterCombat.Health;
		} else {
			characterCombat.Health = newHealthValue;
			return -1;
		}
	}
}