using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character) : base(characterName, character) {
		// spacebar does attacks (similar to player movement and keys)
	}

	// used for movement
	void FixedUpdate() {
		if(Input.GetKeyDown("space")) {

		}
	}
}

