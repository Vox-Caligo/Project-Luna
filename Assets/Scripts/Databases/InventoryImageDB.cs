using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryImageDB : MonoBehaviour {

	private Dictionary<string, string> allInventoryImages;

	void Start() {
		allInventoryImages = new Dictionary<string, string> ();
		allInventoryImages.Add ("Sword", "InventoryImages/Sword");
		allInventoryImages.Add ("Axe", "InventoryImages/Axe");
	}

	public string getImage(string inventoryItem) {
		if(allInventoryImages.ContainsKey(inventoryItem)) {
			return allInventoryImages[inventoryItem];
		}

		print("Inventory item does not exist");
		return null;
	}
}
