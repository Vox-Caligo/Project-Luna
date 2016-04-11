using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

class InventoryItemDB {
	public string ImagePath { get; set; }
	public string Description { get; set; }
	public string Effects { get; set; }

	public InventoryItemDB(string imagePath, string description, string effects) {
		ImagePath = imagePath;
		Description = description;
		Effects = effects;
	}
}

public class InventoryItemsDB : MonoBehaviour {

	private Dictionary<string, InventoryItemDB> allInventoryImages;

	void Start() {
		allInventoryImages = new Dictionary<string, InventoryItemDB> ();
		allInventoryImages.Add ("Sword", new InventoryItemDB("InventoryImages/Sword", "A sword", "Dapper"));
		allInventoryImages.Add ("Axe", new InventoryItemDB("InventoryImages/Axe", "An axe", "Bloodthirst"));
	}

	public string getValue(string inventoryItem, string soughtValue) {
		if(allInventoryImages.ContainsKey(soughtValue)) {
			switch (soughtValue) {
			case "Image":
				return allInventoryImages[soughtValue].ImagePath;
			case "Description":
				return allInventoryImages[soughtValue].Description;
			case "Effects":
				return allInventoryImages[soughtValue].Effects;
			}
		} 

		print("Inventory item does not exist");
		return null;
	}
}
