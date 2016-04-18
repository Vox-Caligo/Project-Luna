using UnityEngine;
using System.Collections;

public class PlayerCombat : Combat
{	
	private int experiencePointsGained;

	public PlayerCombat(string characterName, GameObject character, int karma, string characterWeapon) : base(characterName, character, characterWeapon) {
		this.karma = karma;
	}

	public void updatePlayerCombat(int currentDirection, bool attackKeyPressed) {
		if (this.health <= 0) {
			print ("The Player Is Dead");
		} else {
			if(attackKeyPressed) {
				attacking (currentDirection);
			}

			updateCombat (currentDirection);

			if(inCombat) {
				if(!combatCooldownTimer.runningTimerCountdown()) {
					inCombat = false;
				}
			} else {
				if(!regenerationTimer.runningTimerCountdown()) {
					regeneration();
				}
			}
		}
	}

	public int Karma {
		get { return karma; }
		set { karma = value; }
	}
}