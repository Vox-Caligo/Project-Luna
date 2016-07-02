using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * (Put on Database GameObject) Holds every item that may
 * be displayed in the visual inventory. This includes the
 * name, image path, description, effects, and item type.
 */
public class SoundDB : MonoBehaviour {

    private Dictionary<string, string> allSounds;

    // sets the dictionary
    void Awake() {
        allSounds = new Dictionary<string, string>();
        // songs
        allSounds.Add("Test Theme", "Sounds/Music/Megalovania");
        allSounds.Add("Test Theme 2", "Sounds/Music/Trixie's Trix (Short Edit)");

        // effects
        allSounds.Add("Slash 1", "Sounds/Effects/Attacks/Melee/Sword/Slashing/Slashing1");
        allSounds.Add("Slash 2", "Sounds/Effects/Attacks/Melee/Sword/Slashing/Slashing2");

        // movement
        // footsteps
    }

    // returns a value for the given item
    public string getSound(string inventoryItem) {
        if (allSounds.ContainsKey(inventoryItem)) {
            return allSounds[inventoryItem];
        }

        print("Sound does not exist");
        return null;
    }
}
