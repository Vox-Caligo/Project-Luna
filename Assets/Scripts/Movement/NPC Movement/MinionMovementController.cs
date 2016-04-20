using UnityEngine;
using System.Collections;

/**
 * Controls movement for a minion npc
 */
public class MinionMovementController : DefaultMovementController
{
	// finds the player as a target point
	public MinionMovementController(string characterName, GameObject character) : base(characterName, character) { 
		// can change functions here by calling the different ones needed
		targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	// runs the parents script
	public override void runScript() {
		base.runScript();
	}
}