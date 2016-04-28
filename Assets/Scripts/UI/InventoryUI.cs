using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * The visual representation of the inventory
 * that the player currently has. This includes
 * allowing descriptions/effects as well as what
 * is equipped to be shown.
 */ 
public class InventoryUI : MonoBehaviour
{
	// shows whether it is visible or not
	private bool invisible = true;

	// information for each spot of the inventory
	public GameObject[] inventorySpots = new GameObject[10];
	private Image[] inventoryImages = new Image[10];
	private Image[] backgroundImages = new Image[10];
	private Button[] inventoryButtons = new Button[10];
	private string[] itemNames = new string[10];
	private bool[] equippedItems = new bool[10];

	// connection to item database
	private InventoryItemsDB itemDB;

	// the inventory that is being pulled from
	private Inventory parentInventory;

	// selected buttons variables
	private int previouslySelectedButton = -1;

	// text areas to display information about items
	public Text inventoryItemName;
	public Text inventoryItemDescription;
	public Text inventoryItemEffects;

	// used colors for highlighting purposes
	private Color32 backgroundGrey = new Color32 (94, 94, 94, 255);
	private Color32 backgroundBlue = new Color32 (48, 70, 121, 255);
	private Color32 backgroundYellow = new Color32 (165, 162, 81, 255);

	// when starting a lot of the previous variables are set
	void Awake() {
		// gets the current item database
		itemDB = GameObject.Find ("Databases").GetComponent<InventoryItemsDB> ();

		// runs through the inventory slots and stores them as well as attaching a click listener
		for (int i = 0; i < inventorySpots.Length; i++) {
			inventoryImages [i] = inventorySpots [i].transform.FindChild("Slot Image").GetComponent<Image>();
			inventoryButtons[i] = inventorySpots [i].GetComponent<Button> ();
			backgroundImages [i] = inventorySpots [i].GetComponent<Image> ();

			int storedButton = i;
			inventoryButtons [i].onClick.AddListener (() => {
				itemSlotSelected(storedButton);
			});
		}
	}

	// when an item is clicked it is responded to by either showing descriptions or equipping
	private void itemSlotSelected(int selectedButton) {
		// checks if the spot is visible
		if (!invisible && inventoryImages [selectedButton].color.a == 1) {
			// checks if the button has been previously clicked
			if (previouslySelectedButton != selectedButton) {
				// resets the previously selected button to grey
				if (previouslySelectedButton >= 0 && !equippedItems[previouslySelectedButton]) {
					backgroundImages [previouslySelectedButton].color = backgroundGrey;
				}

				// selects and applies the effects of the selected item, including changing
				// the color of the spot
				previouslySelectedButton = selectedButton;
				inventoryItemName.text = itemDB.getValue (itemNames [selectedButton], "Name");
				inventoryItemDescription.text = itemDB.getValue (itemNames [selectedButton], "Description");
				inventoryItemEffects.text = itemDB.getValue (itemNames [selectedButton], "Effects");
				backgroundImages [selectedButton].color = backgroundBlue;
			} else {
				// equips the item if selected twice
				equipItem (selectedButton);
			}
		}
	}

	// adds an item to the inventory (visually)
	public void addItem(int itemSlot, string itemName) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, true);
		inventoryImages[itemSlot].sprite = Resources.Load (itemDB.getValue(itemName, "Image"), typeof(Sprite)) as Sprite;
		itemNames [itemSlot] = itemName;
	}

	// removes an item from the inventory (visually)
	public void removeItem(int itemSlot) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, false);
	}

	// equips an item that has been selected twice
	public void equipItem(int selectedButton) {
		for (int i = equippedItems.Length - 1; i >= 0; i--) {
			if (equippedItems [i]) {
				if (itemDB.getValue (itemNames [i], "Type") == itemDB.getValue (itemNames [selectedButton], "Type")) {
					backgroundImages [i].color = backgroundGrey;
					equippedItems [i] = false;
				}
			}
		}

		// applies a color to signify selection
		equippedItems[selectedButton]= true;
		backgroundImages [selectedButton].color = backgroundYellow;
		parentInventory.equipOrUseItem (selectedButton);
	}

	// turns the inventory display on and off
	private Color changeVisibility(Color givenItem, bool makeVisible) {
		Color itemSlotAlpha = givenItem;

		if (makeVisible) {
			itemSlotAlpha.a = 1;
			return itemSlotAlpha;
		}

		previouslySelectedButton = -1;
		itemSlotAlpha.a = 0;
		return itemSlotAlpha;
	}

	// things to do when the inventory is hidden
	public void makeInventoryInvisible() {
		inventoryItemName.text = "";
		inventoryItemDescription.text = "";
		inventoryItemEffects.text = "";

		if (previouslySelectedButton != -1 && backgroundImages [previouslySelectedButton].color == backgroundBlue) {
			backgroundImages [previouslySelectedButton].color = backgroundGrey;
			previouslySelectedButton = -1;
		}
	}

	// checks if the display is visible
	public bool Invisible {
		get { return invisible; }
		set { invisible = value; }
	}

	// gets the inventory (non-visual)
	public Inventory ParentInventory {
		set { parentInventory = value; }
	}

	// gets the currently equipped items
	public bool[] EquippedItems {
		get { return equippedItems; }
	}
}