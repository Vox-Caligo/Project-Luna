using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class StandardStats {
	public int Health { get; set; }
	public int Damage { get; set; }
	public int Defense { get; set; }
}

public class StatDB : MonoBehaviour {

	private Dictionary<string, StandardStats> allCharacters;

	void Start() {
		allCharacters = new Dictionary<string, StandardStats> ();
		allCharacters.Add ("Player", new StandardStats { Health = 20, Damage = 10, Defense = 10 });
		allCharacters.Add ("Minion", new StandardStats { Health = 10, Damage = 5, Defense = 5 });
	}

	public int getValue(string character, string soughtValue) {
		switch (soughtValue) {
		case "Health":
			return allCharacters[character].Health;
		case "Damage":
			return allCharacters[character].Damage;
		case "Defense":
			return allCharacters[character].Defense;
		}

		return -1;
	}
}
