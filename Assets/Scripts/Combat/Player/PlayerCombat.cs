using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character) : base(characterName, character) {
		// spacebar does attacks (similar to player movement and keys)
	}

	// used for movement
	protected override void FixedUpdate() {
		if(Input.GetKeyDown("space")) {
			attacking (3);
		}
		print ("PTT: " + timerTick + " but at the same time atkRng: " + attackRange);
		base.FixedUpdate ();
	}
}