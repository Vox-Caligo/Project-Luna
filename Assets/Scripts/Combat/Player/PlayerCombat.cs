using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character) : base(characterName, character) {
		// spacebar does attacks (similar to player movement and keys)
	}

	private void attack() {

	}

	// used for movement
	void FixedUpdate() {
		attack ();
	}
}

