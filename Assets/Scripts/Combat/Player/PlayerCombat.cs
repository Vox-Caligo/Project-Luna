using UnityEngine;
using System.Collections;

/**
 * Class for having the player being able to be used in combat
 */
public class PlayerCombat : Combat
{	
	private int experiencePointsGained;

	// creates a default player
	public PlayerCombat(string characterName, GameObject character, int karma, string characterWeapon) : base(characterName, character, characterWeapon) {
		this.karma = karma;
	}

	// used to have the player in combar
	public void updatePlayerCombat(int currentDirection, bool attackKeyPressed) {
		if (this.health.CurrentHealth <= 0) {
			// kills the  player if they have no health
			print ("The Player Is Dead");
		} else {
			if(attackKeyPressed) {
				// attacks if the attack key was pressed
				attacking (currentDirection);
			}

			// updates combat
			updateCombat (currentDirection);

			// starts regeneration of health/mana if they have been out of combat long enough
			if(inCombat) {
				if(!combatCooldownTimer.runningTimerCountdown()) {
					inCombat = false;
				}
			} else {
                health.regeneration();
                mana.regeneration();
			}
		}
	}

	// used for checking/setting current karma
	public int Karma {
		get { return karma; }
		set { karma = value; }
	}
}