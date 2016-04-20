using UnityEngine;
using System.Collections;

/**
 * A button that can be weighed down by either anything
 * or by a specific item that is needed. Can tell
 * whether or not it has objects on or off.
 */
public class WeightedButton : MonoBehaviour
{
	public int buttonGroup;									// the button group this is a part of
	public bool buttonRequiresSpecificItem = false;			// if it needs a specific item on
	private bool isWeightedDown = false;					// if the item is weighed down
	private ArrayList objectsCurrentlyOn = new ArrayList();	// all objects currently on the button
	private WeightedButtonController buttonController;		// the controller for this button

	public WeightedButton() {}

	// if an item enters the button area, add it to objects on
	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			objectsCurrentlyOn.Add(col.gameObject);
		}
	}

	// if an item leaves the button, remove it from objects currently on
	private void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			objectsCurrentlyOn.Remove(col.gameObject);
		}
	}

	// checks if the button is weighted down
	protected void Update () {
		if(!isWeightedDown) {
			if(objectsCurrentlyOn.Count > 0) {
				// if anything can be on the button, weigh it down
				if(!buttonRequiresSpecificItem) {
					isWeightedDown = true;
					buttonController.buttonHasBeenManipulated(true);
				} else {
					// check if a specific item is on and, if so, weigh it down
					foreach(GameObject objectCurrentlyOn in objectsCurrentlyOn) {
						MoveableBlock blocks = objectCurrentlyOn.GetComponent<MoveableBlock>();
						if(blocks != null && blocks.buttonGroup == buttonGroup) {
							isWeightedDown = true;
							buttonController.buttonHasBeenManipulated(true);
							break;
						}
					}
				}
			}
		} else if(isWeightedDown) {
			if (objectsCurrentlyOn.Count <= 0) {
				// if nothing is on the button and it needs anything
				removedWeight();
			} else if (buttonRequiresSpecificItem) {
				// checks if the specific item is on

				if (objectsCurrentlyOn.Count == 1 && ((GameObject)objectsCurrentlyOn[0]).GetComponent<PlayerMaster>() != null) {
					// if only the player is on
					removedWeight();
				} else {
					// if the appropriate block is no longer on
					bool missingKeyBlock = true;
					foreach(GameObject objectCurrentlyOn in objectsCurrentlyOn) {
						MoveableBlock blocks = objectCurrentlyOn.GetComponent<MoveableBlock>();

						if (blocks != null && blocks.buttonGroup == buttonGroup) {
							missingKeyBlock = false;
							break;
						}
					}

					// if the appropriate block is no longer on, remove the weight
					if(missingKeyBlock) {
						print ("Missing key block");
						removedWeight();
					}
				}
			}
		}
	}

	// remove the effects of weight being on this button
	private void removedWeight() {
		isWeightedDown = false;
		buttonController.buttonHasBeenManipulated(false);
	}

	// checks if weighted down
	public bool IsWeightedDown {
		get {return isWeightedDown;}
	}

	// sets the controller
	public WeightedButtonController ButtonController {
		set {buttonController = value;}
	}
}

