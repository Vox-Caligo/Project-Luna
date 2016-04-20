using UnityEngine;
using System.Collections;

/**
 * An item(s) that requires another item to be placed inside
 * before anything can happen (needs to be on an object)
 */
public class ItemReceptacle : InteractableItem
{
	// items that are being requested
	public string[] soughtItems;

	// which item the player needs to currently add
	private int soughtItemRunner = 0;

	// the player's inventory
	private Inventory playerInventory;

	// if the receptacle has all items needed
	private bool receptacleIsFull = false;

	// if an item is needed, it checks the players inventory for it and places
	// it in if so, otherwise tells how it doesn't exist.
	public override void onInteraction() {
		if (playerInventory == null) {
			playerInventory = GameObject.FindWithTag ("Player").GetComponent<PlayerMaster> ().PlayerInventory;
		}

		if (!receptacleIsFull) {
			if (soughtItemRunner < soughtItems.Length) {
				if (playerInventory.placeItem (soughtItems [soughtItemRunner])) {
					print ("Put in " + soughtItems [soughtItemRunner]);
					soughtItemRunner++;

					if (soughtItemRunner == soughtItems.Length) {
						receptacleIsFull = true;
						placementFinished ();
					}
				} else {
					print ("The item isn't in the inventory");
				}
			}
		}
	}

	// when it has all needed items, something happens
	private void placementFinished() {
		print ("All items are put in");
	}
}