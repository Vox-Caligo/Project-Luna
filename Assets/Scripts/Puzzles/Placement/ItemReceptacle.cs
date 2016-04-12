using UnityEngine;
using System.Collections;

public class ItemReceptacle : InteractableItem
{
	public string[] soughtItems;
	private int soughtItemRunner = 0;

	private Inventory playerInventory;
	private bool receptacleIsFull = false;

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

	private void placementFinished() {
		print ("All items are put in");
	}
}