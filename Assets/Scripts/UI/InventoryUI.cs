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
	private InventoryItemsDB itemDB;

	// selected buttons variables
	private int previouslySelectedButton = -1;

	public Text inventoryItemName;
	public Text inventoryItemDescription;
	public Text inventoryItemEffects;

	void Start() {
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
			print (previouslySelectedButton);
			if (previouslySelectedButton != selectedButton) {
				previouslySelectedButton = selectedButton;
				inventoryItemDescription.text = itemDB.getValue (itemNames [selectedButton], "Description");
				inventoryItemEffects.text = itemDB.getValue (itemNames [selectedButton], "Effects");
				print ("Selected Button");
			} else {
				print ("Equipped " + inventorySpots[selectedButton].name);
			}
		}
	}

	public void addItem(int itemSlot, string itemName) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, true);
		inventoryImages[itemSlot].sprite = Resources.Load (itemDB.getValue(itemName, "Image"), typeof(Sprite)) as Sprite;
		print (inventoryImages [itemSlot]);
		itemNames [itemSlot] = itemName;
	}

	public void removeItem(int itemSlot) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, false);
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
}

