using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for items
 */
class InventoryItemDB {
	public string ItemName { get; set; }
	public string ImagePath { get; set; }
	public string Description { get; set; }
	public string Effects { get; set; }
	public string Type { get; set; }

	public InventoryItemDB(string itemName, string imagePath, string description, string effects, string type) {
		ItemName = itemName;
		ImagePath = imagePath;
		Description = description;
		Effects = effects;
		Type = type;
	}
}

/**
 * (Put on Database GameObject) Holds every item that may
 * be displayed in the visual inventory. This includes the
 * name, image path, description, effects, and item type.
 */
public class InventoryItemsDB : MonoBehaviour {

	private Dictionary<string, InventoryItemDB> allInventoryImages;

	// sets the dictionary
	void Awake() {
		allInventoryImages = new Dictionary<string, InventoryItemDB> ();
		allInventoryImages.Add ("Starter Sword", new InventoryItemDB("Starter Sword", "InventoryImages/Sword", "A sword", "Dapper", "Weapon"));
		allInventoryImages.Add ("Starter Axe", new InventoryItemDB("Starter Axe", "InventoryImages/Axe", "An axe", "Bloodthirst", "Weapon"));
        allInventoryImages.Add("Instant Health Potion", new InventoryItemDB("Instant Health Potion", "InventoryImages/Health Potion", "A potion to heal health", "Instant Health", "Instant Health"));
        allInventoryImages.Add("Instant Mana Potion", new InventoryItemDB("Instant Mana Potion", "InventoryImages/Mana Potion", "A potion to return mana", "Instant Mana", "Instant Mana"));
        allInventoryImages.Add("Regenerative Health Potion", new InventoryItemDB("Regenerative Health Potion", "InventoryImages/Health Potion", "A potion to heal health", "Regenerative Health", "Regenerative Health"));
        allInventoryImages.Add("Regenerative Mana Potion", new InventoryItemDB("Regenerative Mana Potion", "InventoryImages/Mana Potion", "A potion to return mana", "Regenerative Mana", "Regenerative Mana"));
    }

	// returns a value for the given item
	public string getValue(string inventoryItem, string soughtValue) {
		if(allInventoryImages.ContainsKey(inventoryItem)) {
			switch (soughtValue) {
			case "Name":
				return allInventoryImages[inventoryItem].ItemName;
			case "Image":
				return allInventoryImages[inventoryItem].ImagePath;
			case "Description":
				return allInventoryImages[inventoryItem].Description;
			case "Effects":
				return allInventoryImages[inventoryItem].Effects;
			case "Type":
				return allInventoryImages[inventoryItem].Type;
			}
		} 

		print("Inventory item " + inventoryItem + " does not exist");
		return null;
	}
}
