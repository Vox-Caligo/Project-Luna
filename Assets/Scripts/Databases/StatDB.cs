using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class StandardStats {
	public int Health { get; set; }
	public int Defense { get; set; }
}

public class StatDB : MonoBehaviour {

	private Dictionary<string, StandardStats> allCharacters;

	void Start() {
		allCharacters = new Dictionary<string, StandardStats> ();
		allCharacters.Add ("Player", new StandardStats { Health = 20, Defense = 10 });
		allCharacters.Add ("Minion", new StandardStats { Health = 10, Defense = 5 });
	}

	public int getValue(string character, string soughtValue) {
		if(allCharacters.ContainsKey("Player")) {
			switch (soughtValue) {
			case "Health":
				return allCharacters[character].Health;
			case "Defense":
				return allCharacters[character].Defense;
			}
		}

		print(character + " does not exist in Character Database");
		return -1;
	}
}
