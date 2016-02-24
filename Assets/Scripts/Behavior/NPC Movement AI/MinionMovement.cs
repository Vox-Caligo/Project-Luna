using UnityEngine;
using System.Collections;

public class MinionMovement : DefaultMovement
{
	public MinionMovement(string characterName, GameObject character) : base(characterName, character) { 
		// can change functions here by calling the different ones needed
	}

	public override void runScript() {
		if(!pursuingFunctions.Dashing)
			pursuingFunctions.TargetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

		base.runScript();
	}
}