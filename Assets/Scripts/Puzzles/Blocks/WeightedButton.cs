using UnityEngine;
using System.Collections;

public class WeightedButton : MonoBehaviour
{
	public int buttonGroup;
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
	protected void FixedUpdate () {
		if(objectsCurrentlyOn.Count > 0) {
			if(!isWeightedDown) {
				isWeightedDown = true;
				buttonController.buttonHasBeenManipulated(true);
			}
		} else if(isWeightedDown) {
			isWeightedDown = false;
			buttonController.buttonHasBeenManipulated(false);
		}
	}

	public bool IsWeightedDown {
		get {return isWeightedDown;}
	}

	public WeightedButtonController ButtonController {
		set {buttonController = value;}
	}
}

