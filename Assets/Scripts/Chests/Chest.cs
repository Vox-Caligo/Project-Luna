using UnityEngine;
using System.Collections;

/**
 * An item(s) that contains items inside of it that can be pulled out.
 * Also creates a UI for it
 */
public class Chest : InteractableItem
{
    public ArrayList chestInventory;

    private ShopInventoryUI shopUIScript;
    private CanvasGroup shopUIGroup;

    private bool chestIsOpen = false;

	// opens the UI and acts as a form of
	public override void onInteraction() {
		if (shopUIGroup == null) {
            GameObject inventoryUI = Instantiate(Resources.Load("UI/Inventory")) as GameObject;
            shopUIGroup = inventoryUI.GetComponent<CanvasGroup>();
            shopUIScript = inventoryUI.GetComponent<ShopInventoryUI>();
        }

        chestIsOpen = !chestIsOpen;

        if (chestIsOpen) {
            // access inventory
            shopUIGroup.alpha = 1;
        } else {
            // close inventory
            shopUIGroup.alpha = 0;
        }
	}

    public void activateComponent() {
        this.gameObject.SetActive(true);
    }
}