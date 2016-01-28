using UnityEngine;
using System.Collections;

public class MinionMovement : DefaultMovement
{
	public MinionMovement(GameObject character) : base(character) { 
		currentAction = 1;
		movementSpeed = 1;
		// can change functions here by calling the different ones needed
	}

	public override void runScript() {
		pursuingFunctions.TargetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;
		base.runScript();
	}
}