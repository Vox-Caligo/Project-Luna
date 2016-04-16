﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// holds some standard information about the item and where it's located
class InventoryItem {
	string name;
	string type;
	
	public InventoryItem(string name, string type) {
		this.name = name;
		this.type = type;
	}
	
	public string Name {
		get { return name; }
		set { name = value;}
	}

	public string Type {
		get { return type; }
		set { type = value;}
	}
}

public class Inventory : MonoBehaviour
{
	private PlayerMaster currentPlayer;
	private InventoryItem[] personalInventory = new InventoryItem[10];
	private InventoryUI inventoryUIScript;
	private CanvasGroup inventoryUIGroup;
	private int inventoryRunner = 0;
	private bool mouseDown = false;
	private WeaponDB weaponData;

	public Inventory(string storedInventory = null) {
		GameObject inventoryUI = Instantiate(Resources.Load("UI/Inventory")) as GameObject;
		inventoryUIGroup = inventoryUI.GetComponent<CanvasGroup>();
		inventoryUIScript = inventoryUI.GetComponent<InventoryUI>();
		inventoryUIScript.ParentInventory = this;
		weaponData = GameObject.Find ("Databases").GetComponent<WeaponDB> ();

		currentPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster> ();

		if (storedInventory != null) {
			readInInventory (storedInventory);
		}
	}

	private void readInInventory(string storedInventory) {
		string[] currentInventory = storedInventory.Split (new string[] { "NEXT" }, System.StringSplitOptions.None);

		for (int i = 0; i < personalInventory.Length; i++) {
			if (currentInventory [i] != "EMPTY") {
				inventoryRunner = i;

				if (currentInventory [i].Contains ("UNEQUIPPED")) {
					string[] newItem = currentInventory [i].Split (new string[] { "UNEQUIPPED" }, System.StringSplitOptions.None);
					addItemFromInventory (newItem [0], newItem [1]);
				} else {
					string[] newItem = currentInventory [i].Split (new string[] { "EQUIPPED" }, System.StringSplitOptions.None);
					addItemFromInventory (newItem [0], newItem [1]);
					inventoryUIScript.equipItem (i);
				}
			}
		}

		findNearestEmptyItemSlot();
	}

	// adds an item to the players inventory
	public bool addItemFromInventory(string itemName, string itemType) {
		if(inventoryRunner >= 0 && inventoryRunner < personalInventory.Length) {
			personalInventory[inventoryRunner] = new InventoryItem(itemName, itemType);
			inventoryUIScript.addItem(inventoryRunner, itemName);
			findNearestEmptyItemSlot();
			return true;
		}

		print("Cannot fit item in inventory");
		return false;
	}

	// removes an item from the players inventory
	public bool removeItemFromInventory(string itemName) {
		for(int i = 0; i < personalInventory.Length; i++) {
			if(personalInventory[i] != null && personalInventory[i].Name == itemName) {
				personalInventory[i] = null;
				inventoryUIScript.removeItem(i);
				findNearestEmptyItemSlot();
				return true;
			}
		}

		print("Cannot find item in inventory");
		return false;
	}

	// finds the earliest empty slot
	private void findNearestEmptyItemSlot() {
		bool foundNearestSpot = false;

		for(int i = 0; i < personalInventory.Length; i++) {
			if(personalInventory[i] == null) {
				foundNearestSpot = true;
				inventoryRunner = i;
				break;
			}
		}

		if(!foundNearestSpot) {
			inventoryRunner = -1;
		}
	}

	public void visibility() {
		inventoryUIScript.Invisible = !inventoryUIScript.Invisible;

		if (inventoryUIScript.Invisible) {
			inventoryUIGroup.alpha = 0;
		} else {
			inventoryUIGroup.alpha = 1;
		}
	}

	public void equipOrUseItem(int itemLocation) {
		// call databases for what item it is/what it does and apply it to the player
		if (personalInventory [itemLocation].Type == "Weapon") {
			currentPlayer.currentCharacterCombat().Damage = (int)weaponData.getValue (personalInventory [itemLocation].Name, "Damage");
			currentPlayer.currentCharacterCombat().AttackDelay = weaponData.getValue (personalInventory [itemLocation].Name, "Speed");
			currentPlayer.currentCharacterCombat().AttackRange = weaponData.getValue (personalInventory [itemLocation].Name, "Length");
			currentPlayer.currentCharacterCombat().AttackWidth = weaponData.getValue (personalInventory [itemLocation].Name, "Width");
		}
	}

	public bool placeItem(string checkedItemName) {
		for(int i = 0; i < personalInventory.Length; i++) {
			if(personalInventory[i] != null && personalInventory[i].Name == checkedItemName) {
				removeItemFromInventory(checkedItemName);
				return true;
			}
		}

		return false;
	}

	public string storeInventory() {
		string currentInventory = "";
		bool[] equippedItems = inventoryUIScript.EquippedItems;

		for (int i = 0; i < personalInventory.Length; i++) {
			if (personalInventory [i] == null) {
				currentInventory += "EMPTY";
			} else {
				currentInventory += personalInventory [i].Name;

				string equippedStatus;

				if (equippedItems [i]) {
					equippedStatus = "EQUIPPED";
				} else {
					equippedStatus = "UNEQUIPPED";
				}

				currentInventory += equippedStatus + personalInventory [i].Type;
			}

			currentInventory += "NEXT";
		}

		return currentInventory;
	}

	public bool inventoryIsInvisible() {
		return inventoryUIScript.Invisible;
	}
}
