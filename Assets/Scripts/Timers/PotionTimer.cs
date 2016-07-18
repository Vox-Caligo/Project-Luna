using UnityEngine;
using System.Collections;

/**
 * A clock that is used to countdown before resetting
 * and allowing more countdowns.
 */
public class PotionTimer : MonoBehaviour {

    private PlayerMaster currentPlayer;

    private string type;

    private float timer;
    private int seconds;
    private int secondTicks = 0;
    private int manipulatedValue;
    
	// sets the current and max timer
	public PotionTimer(PlayerMaster currentPlayer, string type, int seconds, int manipulatedValue) {
        timer = 0.0f;
        this.currentPlayer = currentPlayer;
        this.type = type;
        this.seconds = seconds;
        this.manipulatedValue = manipulatedValue;
    }

    public bool updateEffect() {
        if(secondTicks < seconds) {
            timer += Time.deltaTime;

            if(timer % 1.0f > 0) {
                secondTicks++;
                timer -= 1.0f;
                
                if (type == "Health") {
                    currentPlayer.currentCharacterCombat().Health.addHealth(manipulatedValue);
                } else {
                    currentPlayer.currentCharacterCombat().Mana.addMana(manipulatedValue);
                }
            }

            return true;
        } else {
            return false;
        }
    }
}