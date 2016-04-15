using UnityEngine;
using System.Collections;

public class PlayerStorage : MonoBehaviour
{
	public int retrievePlayerKarma() {
		if (PlayerPrefs.GetInt ("Karma") != null) {
			return PlayerPrefs.GetInt ("Karma");
		}

		return 0;
	}

	public string retrievePlayerInventory() {
		if (PlayerPrefs.GetString ("Inventory") != null) {
			return PlayerPrefs.GetString ("Inventory");
		}

		return null;
	}

	public void storePlayer(int storingKarma, string inventory) {
		PlayerPrefs.SetInt ("Karma", storingKarma);
		PlayerPrefs.SetString ("Inventory", inventory);
	}

	public void newGame() {
		PlayerPrefs.SetInt ("Karma", 0);
	}
}