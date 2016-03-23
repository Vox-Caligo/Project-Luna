using UnityEngine;
using System.Collections;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character, string characterWeapon) : base(characterName, character, characterWeapon) {
	}

	public void updatePlayerCombat(int currentDirection) {
		if(Input.GetKeyDown("space")) {
			attacking (currentDirection);
		}
		updateCombat (currentDirection);
	}
}