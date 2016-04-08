using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : MonoBehaviour
{
	private bool invisible = true;
	public Image[] inventorySpots = new Image[10];
	private InventoryImageDB imageDB;

	void Start() {
		imageDB = GameObject.Find ("Databases").GetComponent<InventoryImageDB> ();
	}

	public void addItem(int itemSlot, string itemName) {
		inventorySpots[itemSlot].color = changeVisibility(inventorySpots[itemSlot].color, true);
		inventorySpots[itemSlot].sprite = Resources.Load (imageDB.getImage(itemName), typeof(Sprite)) as Sprite;
	}

	public void removeItem(int itemSlot) {
		inventorySpots[itemSlot].color = changeVisibility(inventorySpots[itemSlot].color, false);
	}

	private Color changeVisibility(Color givenItem, bool makeVisible) {
		Color itemSlotAlpha = givenItem;

		if(makeVisible) {
			itemSlotAlpha.a = 1;
			return itemSlotAlpha;
		}

		itemSlotAlpha.a = 0;
		return itemSlotAlpha;
	}

	public bool Invisible {
		get { return invisible; }
		set { invisible = value; }
	}
}

