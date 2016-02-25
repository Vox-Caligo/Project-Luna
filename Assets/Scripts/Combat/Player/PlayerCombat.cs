using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character) : base(characterName, character) {
	}

	public void updatePlayerCombat(int currentDirection) {
		if(Input.GetKeyDown("space")) {
			attacking (currentDirection);
		}
		base.FixedUpdate ();
	}
}