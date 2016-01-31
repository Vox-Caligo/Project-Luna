using UnityEngine;
using System.Collections;

public class DefaultAI : MonoBehaviour
{
	public string characterName;
	protected NpcCombat characterCombat;
	protected NpcMovement characterMovement;
	
	// know when/where to move
	// know when to attack

	// Use this for initialization
	public DefaultAI() {
		characterCombat = new NpcCombat(characterName);
		characterMovement = new NpcMovement(characterName);
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
		processDecisions();
	}
}

