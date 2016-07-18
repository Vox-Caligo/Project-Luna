using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// holds some standard information about the item (name and type)
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

/**
 * Allows storage of all items and places it in the appropriate
 * data structures. Also has access to all scripts of the items.
 */
public class Inventory : MonoBehaviour
{
	private PlayerMaster currentPlayer;
	private InventoryItem[] personalInventory = new InventoryItem[10];
	private InventoryUI inventoryUIScript;
	private CanvasGroup inventoryUIGroup;
	private int inventoryRunner = 0;
	private WeaponDB weaponData;
    private ItemDB itemData;
    private ArrayList potionEffects = new ArrayList();
    private ArrayList potionEffectIcons = new ArrayList();

    // hacky fix location for potion effect icons to be based on
    private Vector3 masterPotionEffectsLocation = new Vector3(37.5f, 658f);

	// sets default variables when created
	public Inventory(string storedInventory = null) {
		/// sets the inventory ui and gets the needed variables
		GameObject inventoryUI = Instantiate(Resources.Load("UI/Inventory")) as GameObject;
		inventoryUIGroup = inventoryUI.GetComponent<CanvasGroup>();
		inventoryUIScript = inventoryUI.GetComponent<InventoryUI>();
		inventoryUIScript.ParentInventory = this;

		// finds the weapon database
		weaponData = GameObject.Find ("Databases").GetComponent<WeaponDB> ();

        // finds the item database
        itemData = GameObject.Find("Databases").GetComponent<ItemDB>();

        // finds the current player
        currentPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster> ();

		// if an inventory exists, reads it in
		if (storedInventory != null) {
			readInInventory (storedInventory);
		}
	}

	// reads in the inventory from a string
	private void readInInventory(string storedInventory) {
		string[] currentInventory = storedInventory.Split (new string[] { "NEXT" }, System.StringSplitOptions.None);

		for (int i = 0; i < personalInventory.Length; i++) {
            // checks to make sure there is an item
			if (currentInventory.Length > i && currentInventory[i] != "EMPTY") {
				inventoryRunner = i;

				// adds the item and has it either be equipped or not
				if (currentInventory [i].Contains ("UNEQUIPPED")) {
					string[] newItem = currentInventory [i].Split (new string[] { "UNEQUIPPED" }, System.StringSplitOptions.None);
                    if (newItem.Length == 2)
                    {
                        addItemFromInventory(newItem[0], newItem[1]);
                    }
				} else {
					string[] newItem = currentInventory [i].Split (new string[] { "EQUIPPED" }, System.StringSplitOptions.None);
                    if (newItem.Length == 2)
                    {
                        addItemFromInventory(newItem[0], newItem[1]);
                        inventoryUIScript.equipItem(i);
                    }
				}
			}
		}

		// finds the next spot for entry for future item placement
		findNearestEmptyItemSlot();
	}

	// adds an item to the players inventory if possible
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

	// removes an item from the players inventory if applicable
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

	// finds the earliest empty slot to insert an item
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

	// turns the visibility of the UI on/off
	public void visibility() {
		inventoryUIScript.Invisible = !inventoryUIScript.Invisible;

		if (inventoryUIScript.Invisible) {
			inventoryUIGroup.alpha = 0;
			inventoryUIScript.makeInventoryInvisible ();
		} else {
			inventoryUIGroup.alpha = 1;
		}
	}

	// when an item is used, applies its effects to the player
	public void equipOrUseItem(int itemLocation) {
        string itemType = personalInventory[itemLocation].Type;

        if (itemType == "Weapon") {
			currentPlayer.currentCharacterCombat ().changeWeapon (personalInventory [itemLocation].Name);
		} else if (itemType.Contains("Health")) {
            if (itemType.Contains("Instant")) {
                currentPlayer.currentCharacterCombat().Health.addHealth(itemData.getItem(itemType).ManipulatedValueAmount);
            } else if (itemType.Contains("Regenerative")) {
                PotionTimer newRegenerativeEffect = new PotionTimer(currentPlayer, "Health", itemData.getItem(itemType).ActiveTime, itemData.getItem(itemType).ManipulatedValueAmount);
                potionEffects.Add(newRegenerativeEffect);

                GameObject newIcon = currentPlayer.PlayerHud.addEffect(itemData.getItem(itemType).AddedEffectImagePath);
                Vector3 itemEffectPosition = newIcon.transform.position;
                potionEffectIcons.Add(newIcon);
            }

            removeItemFromInventory(personalInventory[itemLocation].Name);
        } else if(itemType.Contains("Mana")) {
            if (itemType.Contains("Instant")) {
                currentPlayer.currentCharacterCombat().Mana.addMana(itemData.getItem(itemType).ManipulatedValueAmount);
            } else if (itemType.Contains("Regenerative")) {
                PotionTimer newRegenerativeEffect = new PotionTimer(currentPlayer, "Mana", itemData.getItem(itemType).ActiveTime, itemData.getItem(itemType).ManipulatedValueAmount);
                potionEffects.Add(newRegenerativeEffect);

                GameObject newIcon = currentPlayer.PlayerHud.addEffect(itemData.getItem(itemType).AddedEffectImagePath);
                Vector3 itemEffectPosition = newIcon.transform.position;
                potionEffectIcons.Add(newIcon);
            }

            removeItemFromInventory(personalInventory[itemLocation].Name);
        }
	}

	// places an item into the inventory
	public bool placeItem(string checkedItemName) {
		for(int i = 0; i < personalInventory.Length; i++) {
			if(personalInventory[i] != null && personalInventory[i].Name == checkedItemName) {
				removeItemFromInventory(checkedItemName);
				return true;
			}
		}

		return false;
	}

	// stores the inventory in a continuous string
	public string storeInventory() {
		string currentInventory = "";
		bool[] equippedItems = inventoryUIScript.EquippedItems;

		for (int i = 0; i < personalInventory.Length; i++) {
			if (personalInventory [i] == null) {
				// no item in the slot
				currentInventory += "EMPTY";
			} else {
				// item in that slot
				currentInventory += personalInventory [i].Name;

				string equippedStatus;

				// checks whether the item is currently equipped
				if (equippedItems [i]) {
					equippedStatus = "EQUIPPED";
				} else {
					equippedStatus = "UNEQUIPPED";
				}

				currentInventory += equippedStatus + personalInventory [i].Type;
			}

			// used to denote moving to the next spot
			currentInventory += "NEXT";
		}

		return currentInventory;
	}

    public void updateRegenerativePotions() {
        for(int i = potionEffects.Count - 1; i >= 0; i--) {
            PotionTimer potionTimer = potionEffects[i] as PotionTimer;

            if (!potionTimer.updateEffect()) {
                print("Once");
                GameObject potionEffectIcon = potionEffectIcons[i] as GameObject;
                potionEffectIcons.Remove(potionEffectIcon);
                Destroy(potionEffectIcon);

                potionEffects.RemoveAt(i);
                Destroy(potionTimer);
            }
        }

        for(int i = 0; i < potionEffectIcons.Count; i++) {
            GameObject potionEffectIcon = potionEffectIcons[i] as GameObject;
            potionEffectIcon.transform.position = new Vector3(masterPotionEffectsLocation.x, masterPotionEffectsLocation.y - (i * potionEffectIcon.GetComponent<RectTransform>().rect.height / 2));
        }
    }

	// returns the inventory UI's visiblity
	public bool inventoryIsInvisible() {
		return inventoryUIScript.Invisible;
	}
}
