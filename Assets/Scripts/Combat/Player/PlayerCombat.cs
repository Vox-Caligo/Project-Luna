using UnityEngine;
using System.Collections;

public class PlayerCombat : Combat
{	
	public PlayerCombat(string characterName, GameObject character, string characterWeapon) : base(characterName, character, characterWeapon) {}

	public void updatePlayerCombat(int currentDirection, bool attackKeyPressed) {
		if(attackKeyPressed) {
			attacking (currentDirection);
		}

		if(inCombat) {
			inCombatTimerCountdown();
		} else {
			regenerationTimer();
		}
	}
}