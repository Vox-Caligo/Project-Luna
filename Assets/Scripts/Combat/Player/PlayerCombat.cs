using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character) : base(characterName, character) {
	}

	public void updatePlayerCombat() {
		if(Input.GetKeyDown("space")) {
			attacking (3);
		}
		base.FixedUpdate ();
	}
}