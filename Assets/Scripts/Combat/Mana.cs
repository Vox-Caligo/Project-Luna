using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {
    // properties of the character
    protected int currentMana;
    protected int maxMana;
    protected bool manaRegeneration = false;
    protected UtilTimer regenerationTimer;	// time to regenerate
    
    public Mana(int maxMana) {
        currentMana = maxMana;
        this.maxMana = maxMana;

        regenerationTimer = new UtilTimer(1.5f, 1.5f);
    }

    // restore mana if below the thresholds
    public void regeneration() {
        if (!regenerationTimer.runningTimerCountdown() && manaRegeneration && currentMana < maxMana) {
            currentMana++;
        }
    }

    public void addMana(int addedMana, float maxMana = 0) {
        if (addedMana < 0) {
            manaRegeneration = false;

            if (currentMana > 0) {
                currentMana += addedMana;
            } else {
                currentMana = 0;
            }
        } else if (currentMana < maxMana) {
            currentMana += addedMana;
        }

        print("Manipulated mana: " + currentMana);
    }

    // get/set the character's mana
    public int CurrentMana {
        get { return currentMana; }
        set { currentMana = value; }
    }

    // get/set the character's max mana
    public int MaxMana {
        get { return maxMana; }
        set { maxMana = value; }
    }

    // get/set if mana can regenerate
    public bool ManaRegeneration {
        get { return manaRegeneration; }
        set { manaRegeneration = value; }
    }
}
