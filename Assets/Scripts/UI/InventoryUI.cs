using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
	private bool invisible = true;
	public GameObject[] inventorySpots = new GameObject[10];
	private Image[] inventoryImages = new Image[10];
	private Image[] backgroundImages = new Image[10];
	private Button[] inventoryButtons = new Button[10];
	private string[] itemNames = new string[10];
	private bool[] equippedItems = new bool[10];

	private InventoryItemsDB itemDB;
	private Inventory parentInventory;

	// selected buttons variables
	private int previouslySelectedButton = -1;

	public Text inventoryItemName;
	public Text inventoryItemDescription;
	public Text inventoryItemEffects;

	// used colors for highlighting purposes
	private Color32 backgroundGrey = new Color32 (94, 94, 94, 255);
	private Color32 backgroundBlue = new Color32 (48, 70, 121, 255);
	private Color32 backgroundYellow = new Color32 (165, 162, 81, 255);

	void Awake() {
		itemDB = GameObject.Find ("Databases").GetComponent<InventoryItemsDB> ();

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

	private void itemSlotSelected(int selectedButton) {
		if (inventoryImages [selectedButton].color.a == 1) {
			if (previouslySelectedButton != selectedButton) {
				if (previouslySelectedButton >= 0 && !equippedItems[previouslySelectedButton]) {
					backgroundImages [previouslySelectedButton].color = backgroundGrey;
				}

				previouslySelectedButton = selectedButton;
				inventoryItemName.text = itemDB.getValue (itemNames [selectedButton], "Name");
				inventoryItemDescription.text = itemDB.getValue (itemNames [selectedButton], "Description");
				inventoryItemEffects.text = itemDB.getValue (itemNames [selectedButton], "Effects");
				backgroundImages [selectedButton].color = backgroundBlue;
			} else {
				equipItem (selectedButton);
			}
		}
	}

	public void addItem(int itemSlot, string itemName) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, true);
		inventoryImages[itemSlot].sprite = Resources.Load (itemDB.getValue(itemName, "Image"), typeof(Sprite)) as Sprite;
		itemNames [itemSlot] = itemName;
	}

	public void removeItem(int itemSlot) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, false);
	}

	public void equipItem(int selectedButton) {
		for (int i = equippedItems.Length - 1; i >= 0; i--) {
			if (equippedItems [i]) {
				if (itemDB.getValue (itemNames [i], "Type") == itemDB.getValue (itemNames [selectedButton], "Type")) {
					backgroundImages [i].color = backgroundGrey;
					equippedItems [i] = false;
				}
			}
		}

		equippedItems[selectedButton]= true;
		backgroundImages [selectedButton].color = backgroundYellow;
		parentInventory.equipOrUseItem (selectedButton);
	}

	private Color changeVisibility(Color givenItem, bool makeVisible) {
		Color itemSlotAlpha = givenItem;

		if(makeVisible) {
			itemSlotAlpha.a = 1;
			return itemSlotAlpha;
		}

		previouslySelectedButton = -1;
		itemSlotAlpha.a = 0;
		return itemSlotAlpha;
	}

	public bool Invisible {
		get { return invisible; }
		set { invisible = value; }
	}

	public Inventory ParentInventory {
		set { parentInventory = value; }
	}

	public bool[] EquippedItems {
		get { return equippedItems; }
	}
}