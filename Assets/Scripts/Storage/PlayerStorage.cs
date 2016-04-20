using UnityEngine;
using System.Collections;

/**
 * Used to store the player between games. This
 * includes karma and what the player currently 
 * has in their inventory. It allows quick storage
 * and retrieval of these values.
 */
public class PlayerStorage : MonoBehaviour
{
	// retrieves how much karma the player has
	public int retrievePlayerKarma() {
		if (PlayerPrefs.GetInt ("Karma") != null) {
			return PlayerPrefs.GetInt ("Karma");
		}

		return 0;
	}

	// retrieves which items the player has
	public string retrievePlayerInventory() {
		if (PlayerPrefs.GetString ("Inventory") != null) {
			return PlayerPrefs.GetString ("Inventory");
		}

		return null;
	}

	// stores the current karma and inventory of the player
	public void storePlayer(int storingKarma, string inventory) {
		PlayerPrefs.SetInt ("Karma", storingKarma);
		PlayerPrefs.SetString ("Inventory", inventory);
	}
}