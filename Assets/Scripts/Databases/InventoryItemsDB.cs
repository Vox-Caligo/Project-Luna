using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

public class InventoryItemsDB : MonoBehaviour {

	private Dictionary<string, InventoryItemDB> allInventoryImages;

	void Start() {
		allInventoryImages = new Dictionary<string, InventoryItemDB> ();
		allInventoryImages.Add ("Starter Sword", new InventoryItemDB("Starter Sword", "InventoryImages/Sword", "A sword", "Dapper", "Weapon"));
		allInventoryImages.Add ("Starter Axe", new InventoryItemDB("Starter Axe", "InventoryImages/Axe", "An axe", "Bloodthirst", "Weapon"));
	}

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

		print("Inventory item does not exist");
		return null;
	}
}
