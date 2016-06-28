using UnityEngine;
using System.Collections;

/**
 * An item(s) that requires another item to be placed inside
 * before anything can happen (needs to be on an object)
 */
public class ItemReceptacle : InteractableItem
{
    // quest name that is attached to this item receptacle
    public string questName;

    // items that are being requested
    public string[] soughtItems;

    // determines if the object only shows up during a quest
    public bool questRequired;

    // which item the player needs to currently add
    private int soughtItemRunner = 0;

	// the player's inventory
	private Inventory playerInventory;

    // the player's quest log
    QuestLog playerLog;

	// if the receptacle has all items needed
	private bool receptacleIsFull = false;

    // sets object to be inactive if it is required to be by quest
    void Start() {
        if(questRequired) {
            this.gameObject.SetActive(false);
        }
    }

	// if an item is needed, it checks the players inventory for it and places
	// it in if so, otherwise tells how it doesn't exist.
	public override void onInteraction() {
		if (playerInventory == null) {
			playerInventory = GameObject.FindWithTag ("Player").GetComponent<PlayerMaster> ().PlayerInventory;
            playerLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
        }

		if (!receptacleIsFull) {
			if (soughtItemRunner < soughtItems.Length) {
				if (playerInventory.placeItem (soughtItems [soughtItemRunner])) {
					print ("Put in " + soughtItems [soughtItemRunner]);
					soughtItemRunner++;

					if (soughtItemRunner == soughtItems.Length) {
                        print("All items are put in");
                        receptacleIsFull = true;
                    }

                    playerLog.updateQuest(questName);
                } else {
					print ("The item isn't in the inventory");
				}
			}
		}
	}

    public void activateComponent() {
        this.gameObject.SetActive(true);
    }
}