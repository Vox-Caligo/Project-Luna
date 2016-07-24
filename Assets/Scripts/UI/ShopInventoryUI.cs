using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * The visual representation of the inventory
 * that the player currently has. This includes
 * allowing descriptions/effects as well as what
 * is equipped to be shown.
 */ 
public class ShopInventoryUI : MonoBehaviour
{
	// shows whether it is visible or not
	private bool invisible = true;

	// information for each spot of the inventory
	public GameObject[] inventorySpots = new GameObject[10];
	private Image[] inventoryImages = new Image[10];
	private Image[] backgroundImages = new Image[10];
	private Button[] inventoryButtons = new Button[10];
	private string[] itemNames = new string[10];

	// connection to item database
	private InventoryItemsDB itemDB;

	// the inventory that is being pulled from
	private Inventory parentInventory;

    private Inventory playerInventory;

    // selected buttons variables
    private int previouslySelectedButton = -1;

    // for chest
    public Text chestItemName;
    public Text chestItemDescription;
    public Text chestItemEffects;

    // for shopkeepers
    public Text buyOrSellText;
    public Text price;
    public Text shopkeeperItemName;
    public Text shopkeeperItemDescription;
    public Text shopkeeperItemEffects;

    // used colors for highlighting purposes
    private Color32 backgroundGrey = new Color32 (94, 94, 94, 255);
	private Color32 backgroundBlue = new Color32 (48, 70, 121, 255);
	private Color32 backgroundYellow = new Color32 (165, 162, 81, 255);

    private CanvasGroup uiComponents;

	// when starting a lot of the previous variables are set
	void Awake() {
		// gets the current item database
		itemDB = GameObject.Find ("Databases").GetComponent<InventoryItemsDB> ();
        playerInventory = GameObject.FindWithTag("Player").GetComponent<PlayerMaster>().PlayerInventory;
        uiComponents = this.gameObject.GetComponent<CanvasGroup>();

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
				if (previouslySelectedButton >= 0) {
					backgroundImages [previouslySelectedButton].color = backgroundGrey;
				}

				// selects and applies the effects of the selected item, including changing
				// the color of the spot
				previouslySelectedButton = selectedButton;
				//inventoryItemName.text = itemDB.getValue (itemNames [selectedButton], "Name");
				//inventoryItemDescription.text = itemDB.getValue (itemNames [selectedButton], "Description");
				//inventoryItemEffects.text = itemDB.getValue (itemNames [selectedButton], "Effects");

				// does not unequip any items when viewing stats for equipped 
				if (backgroundImages [selectedButton].color != backgroundYellow) {
					backgroundImages [selectedButton].color = backgroundBlue;
				}
			} else {
				// transfer item to inventory
                // take out cost from gold
			}
		}
	}

	// adds an item to the inventory (visually)
	public void giveItem(int itemSlot, string itemName) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, true);

		inventoryImages[itemSlot].sprite = Resources.Load (itemDB.getValue(itemName, "Image"), typeof(Sprite)) as Sprite;
		itemNames [itemSlot] = itemName;
	}

	// removes an item from the inventory (visually)
	public void removeItem(int itemSlot) {
		inventoryImages[itemSlot].color = changeVisibility(inventoryImages[itemSlot].color, false);
        backgroundImages[itemSlot].color = backgroundGrey;
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

    public void changeUIVisibility(bool chest) {
        invisible = !invisible;

        if (invisible) {
            // access inventory
            uiComponents.alpha = 1;

            if(!chest) {
                chestItemName.text = "";
                chestItemDescription.text = "";
                chestItemEffects.text = "";
                
        buyOrSellText.text = "";
        price.text = "";
        shopkeeperItemName.text = "";
        shopkeeperItemDescription.text = "";
        shopkeeperItemEffects.text = "";
            } else {

            }
        } else {
            // close inventory
            uiComponents.alpha = 0;
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
}