using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	// properties of the character
	protected int currentHealth;
	protected int maxHealth;
	protected bool healthRegeneration = false;
    protected UtilTimer regenerationTimer;	// time to regenerate
    
    public Health(int maxHealth) {
		// sets character health
		this.currentHealth = maxHealth;
        this.maxHealth = maxHealth;
		regenerationTimer = new UtilTimer(1.5f, 1.5f);
	}

    // restore health if below the thresholds
    public void regeneration() {
		// goes until the character is full/half health (depends on current health)
		if(!regenerationTimer.runningTimerCountdown() && healthRegeneration && (currentHealth < maxHealth / 2 || (currentHealth > maxHealth / 2 && currentHealth < maxHealth))) {
            currentHealth++;
		}
	}

    public void addHealth(int addedHealth, int maxHealth = 0) {
        if (addedHealth < 0) {
            healthRegeneration = false;
        }

        if (addedHealth + currentHealth < maxHealth) {
            currentHealth += addedHealth;
        } else {
            currentHealth = maxHealth;
        }

        print("Manipulated health: " + currentHealth);
    }

    // get/set the character's health
    public int CurrentHealth {
        get { return currentHealth; }
        set { currentHealth = value; }
    }

    // get/set the character's max health
    public int MaxHealth {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    // get/set if health can regenerate
    public bool HealthRegeneration {
        get { return healthRegeneration; }
        set { healthRegeneration = value; }
    }
}
