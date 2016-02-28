using UnityEngine;
using System.Collections;

public class MinionMovementController : DefaultMovementController
{
	public MinionMovementController(string characterName, GameObject character) : base(characterName, character) { 
		// can change functions here by calling the different ones needed
		targetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
	}

	public override void runScript() {
		if(!pursuingFunctions.Dashing) {
			pursuingFunctions.TargetPoint = targetPoint;
			nearbyPlayerFunctions.TargetPoint = targetPoint;
		}

		base.runScript();
	}
}