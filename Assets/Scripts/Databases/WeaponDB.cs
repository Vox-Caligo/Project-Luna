using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for weapons
 */
class WeaponStats {
	public int Speed { get; set; }
	public int Damage { get; set; }
	public float Length { get; set; }
	public float Width { get; set; }
}

/**
 * (Put on Database GameObject) A database containing every weapon
 * and its stats (speed, damager, length, width).
 */
public class WeaponDB : MonoBehaviour {

	private Dictionary<string, WeaponStats> allWeapons;

	// sets the dictionary
	void Awake() {
		allWeapons = new Dictionary<string, WeaponStats> ();
		allWeapons.Add ("Starter Sword", new WeaponStats { Speed = 1, Damage = 20, Length = .5f, Width = .2f});
		allWeapons.Add ("Starter Axe", new WeaponStats { Speed = 2, Damage = 10, Length = .5f, Width = .5f});
	}

	// returns a value for a property of the given weapon
	public float getValue(string weapon, string soughtValue) {
		if(allWeapons.ContainsKey(weapon)) {
			switch (soughtValue) {
			case "Speed":
				return allWeapons[weapon].Speed;
			case "Damage":
				return allWeapons[weapon].Damage;
			case "Length":
				return allWeapons[weapon].Length;
			case "Width":
				return allWeapons[weapon].Width;
			default:
				print(weapon + " does not have sought property " + soughtValue);
				break;
			}
		} else {
			print(weapon + " does not exist in Weapon Database");
		}

		return -1;
	}
}
