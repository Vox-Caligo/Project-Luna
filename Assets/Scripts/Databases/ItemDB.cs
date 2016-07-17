using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for weapons
 */
public class ItemStats {
    public bool IsPassive { get; set; }
    public int ActiveTime { get; set; }

    // value for health/mana/damage to be manipulated
    public int ManipulatedValueAmount { get; set; }
	
    public ItemStats(bool isPassive, int manipulatedValueAmount, int activeTime = 0) {
        setItemSets(isPassive, manipulatedValueAmount);

        if (!isPassive) {
            ActiveTime = activeTime;
        }
    }

    private void setItemSets(bool isPassive, int manipulatedValueAmount) {
        IsPassive = isPassive;
        ManipulatedValueAmount = manipulatedValueAmount;
    }
}

/**
 * (Put on Database GameObject) A database containing every weapon
 * and its stats (speed, damager, length, width).
 */
public class ItemDB : MonoBehaviour {

    private Dictionary<string, ItemStats> allItems;

    // sets the dictionary
    void Awake() {
        allItems = new Dictionary<string, ItemStats>();

        // testing health potion
		allItems.Add("Instant Health Potion", new ItemStats(false, 5));
        allItems.Add("Instant Mana Potion", new ItemStats(false, 5));
        allItems.Add("Regenerative Health Potion", new ItemStats(false, 5, 2));
        allItems.Add("Regenerative Mana Potion", new ItemStats(false, 5, 2));
    }

	public ItemStats getItem(string weapon) {
		if (allItems.ContainsKey(weapon)) {
			return allItems[weapon];
		} else {
			print(weapon + " does not exist in Weapon Database");
		}

		return null;
	}
}
