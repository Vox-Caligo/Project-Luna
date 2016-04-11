using UnityEngine;
using System.Collections;

public class WeightedButton : MonoBehaviour
{
	public int buttonGroup;
	public bool buttonRequiresSpecificItem = false;
	private bool isWeightedDown;
	private ArrayList objectsCurrentlyOn = new ArrayList();
	private WeightedButtonController buttonController;

	public WeightedButton() {}

	private void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			objectsCurrentlyOn.Add(col.gameObject);
		}
	}

	private void OnTriggerStay2D(Collider2D col) {
		if(col.gameObject.GetComponent<MoveableBlock>() != null || col.gameObject.GetComponent<PlayerMaster>() != null) {
			isWeightedDown = true;
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
		} else {
			if(!buttonRequiresSpecificItem) {
				isWeightedDown = false;
				buttonController.buttonHasBeenManipulated(false);
			} else {
				bool correctObjectStillOn = false;

				foreach(GameObject objectCurrentlyOn in objectsCurrentlyOn) {
					MoveableBlock blocks = objectCurrentlyOn.GetComponent<MoveableBlock>();
					if(blocks != null && blocks.buttonGroup == buttonGroup) {
						correctObjectStillOn = true;
					}
				}

				if(!correctObjectStillOn) {
					isWeightedDown = false;
					buttonController.buttonHasBeenManipulated(false);
				}
			}
		}
	}

	public bool IsWeightedDown {
		get {return isWeightedDown;}
	}

	public WeightedButtonController ButtonController {
		set {buttonController = value;}
	}
}

