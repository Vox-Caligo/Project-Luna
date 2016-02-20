using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class DefaultAI : MonoBehaviour
{
	public string characterName;
	protected bool hostile = false;
	protected NpcCombat characterCombat;
	protected NpcMovement characterMovement;
	protected int health;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	public virtual void Start() {
		characterCombat = new NpcCombat(characterName, this.gameObject);
		characterMovement = new NpcMovement(characterName, this.gameObject);
		health = DialogueLua.GetActorField(characterName, "Health").AsInt;
	}
	
	protected virtual void processDecisions() {
		// determine if actor should move, attack, or other
		// if move, determine how to move
		// if attack, determine how to attack
		print("I am no minion!");
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		print("Health: " + DialogueLua.GetActorField("Player", "Health").AsInt);
		if(health > 0) {
			processDecisions();
		} else {
			print(characterName + " has died.");
		}
	}

	protected void OnCollisionEnter2D (Collision2D col) {
		characterMovement.npcMovement.respondToCollision (col);
		// do same for combat
	}
}