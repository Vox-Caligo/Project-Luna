using UnityEngine;
using System.Collections;

public class WeightedButton : MonoBehaviour
{
	public int buttonGroup;
	public bool buttonRequiresSpecificItem = false;
	private bool isWeightedDown = false;
	private ArrayList objectsCurrentlyOn = new ArrayList();
	private WeightedButtonController buttonController;

	public WeightedButton() {}

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			objectsCurrentlyOn.Add(col.gameObject);
		}
	}

	private void OnTriggerExit2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			objectsCurrentlyOn.Remove(col.gameObject);
		}
	}

	// Update is called once per frame
	protected void Update () {
		if(!isWeightedDown) {
			if(objectsCurrentlyOn.Count > 0) {
				if(!buttonRequiresSpecificItem) {
					isWeightedDown = true;
					buttonController.buttonHasBeenManipulated(true);
				} else {
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
				removedWeight();
			} else if (buttonRequiresSpecificItem) {
				if (objectsCurrentlyOn.Count == 1 && ((GameObject)objectsCurrentlyOn[0]).GetComponent<PlayerMaster>() != null) {
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

					if(missingKeyBlock) {
						print ("Missing key block");
						removedWeight();
					}
				}
			}
		}
	}

	private void removedWeight() {
		isWeightedDown = false;
		buttonController.buttonHasBeenManipulated(false);
	}

	public bool IsWeightedDown {
		get {return isWeightedDown;}
	}

	public WeightedButtonController ButtonController {
		set {buttonController = value;}
	}
}

