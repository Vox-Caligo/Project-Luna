using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for weapons
 */
public class WeaponStats {
	public string Type { get; set; }
	public float Speed { get; set; }
	public int Damage { get; set; }
	public float Length { get; set; }
	public float Width { get; set; }
    public string[] Sounds { get; set; }
    public float ShotDistance { get; set; }
    public int AOE { get; set; }
	public int ManaCost { get; set; }

	// melee weapon
	public WeaponStats(string type, float speed, int damage, float length, float width, string[] sounds) {
		setWeaponStats(type, speed, damage, length, width, sounds);
	}

	// range weapon
	public WeaponStats(string type, float speed, int damage, float length, float width, string[] sounds, float shotDistance, int aoe = -1, int manaCost = -1) {
		ShotDistance = shotDistance;
		AOE = aoe;
		ManaCost = manaCost;
		setWeaponStats(type, speed, damage, length, width, sounds);
	}

	private void setWeaponStats(string type, float speed, int damage, float length, float width, string[] sounds) {
		Type = type;
		Speed = speed;
		Damage = damage;
		Length = length;
		Width = width;
		Sounds = sounds;
	}
}

/**
 * (Put on Database GameObject) A database containing every weapon
 * and its stats (speed, damager, length, width).
 */
public class WeaponDB : MonoBehaviour {

    private Dictionary<string, WeaponStats> allWeapons;

    // sets the dictionary
    void Awake() {
        allWeapons = new Dictionary<string, WeaponStats>();
		allWeapons.Add("Starter Sword", new WeaponStats("Melee", 1, 2, .5f, .2f, new string[] { "Slash 1", "Slash 2" }));
        //allWeapons.Add("Starter Axe", new WeaponStats { Speed = 2, Damage = 10, Length = .5f, Width = .5f});

        // testing range and magic with this
		//allWeapons.Add("Starter Sword", new WeaponStats("Hybrid Magic", 1.5f, 20, .5f, .2f, new string[] { "Slash 1", "Slash 2" }, 5, 2, 10));
		//allWeapons.Add("Starter Axe", new WeaponStats("Hybrid Magic", 1.5f, 20, .5f, .2f, new string[] { "Slash 1", "Slash 2" }, 0, 2, 5));
    }

	public WeaponStats getWeapon(string weapon) {
		if (allWeapons.ContainsKey(weapon)) {
			return allWeapons[weapon];
		} else {
			print(weapon + " does not exist in Weapon Database");
		}

		return null;
	}

    // returns a value for a property of the given weapon
    public float getValue(string weapon, string soughtValue) {
        if (allWeapons.ContainsKey(weapon)) {
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

	public string getWeaponType(string weapon) {
		if (allWeapons.ContainsKey (weapon)) {
			return allWeapons[weapon].Type;
		} else {
			print(weapon + " does not exist in Weapon Database");
		}

		return "";
	}

    public string getWeaponSound(string weapon) {
        if (allWeapons.ContainsKey(weapon)) {
            int weaponSoundNum = Random.Range(0, allWeapons.Count);
            return allWeapons[weapon].Sounds[weaponSoundNum];
        } else {
            print(weapon + " does not exist in Weapon Database");
            return "";
        }
    }
}
