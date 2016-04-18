using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class StandardStats {
	public int Health { get; set; }
	public int Mana { get; set; }
	public int Defense { get; set; }
	public int Karma { get; set; }
	public int Experience { get; set; }

	public StandardStats(int health, int mana, int defense) {
		this.Health = health;
		this.Mana = mana;
		this.Defense = defense;
	}

	public StandardStats(int health, int mana, int defense, int karma, int experience) {
		this.Health = health;
		this.Mana = mana;
		this.Defense = defense;
		this.Karma = karma;
		this.Experience = experience;
	}
}

public class StatDB : MonoBehaviour {

	private Dictionary<string, StandardStats> allCharacters;

	void Awake() {
		allCharacters = new Dictionary<string, StandardStats> ();
		allCharacters.Add ("Player", new StandardStats(20, 10, 10));
		allCharacters.Add ("Minion", new StandardStats(10, 5, 5, 5, 5));
		allCharacters.Add ("Villager", new StandardStats(10, 5, 5, -5, 5));
	}

	public int getValue(string character, string soughtValue) {
		if(allCharacters.ContainsKey(character)) {
			switch (soughtValue) {
			case "Health":
				return allCharacters[character].Health;
			case "Mana":
				return allCharacters [character].Mana;
			case "Defense":
				return allCharacters[character].Defense;
			case "Karma":
				return allCharacters[character].Karma;
			default: 
				print (soughtValue + " does not exist for the character");
				break;
			}
		}

		print(character + " does not exist in Character Database");
		return -1;
	}
}
