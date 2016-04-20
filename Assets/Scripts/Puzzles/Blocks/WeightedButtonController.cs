using UnityEngine;
using System.Collections;

/**
 * Controlls all buttons in a group and sees if
 * they are all being weighted down correctly
 */
public class WeightedButtonController : MonoBehaviour
{
	// the button group being overseen
	public int buttonGroup;

	// how many activated buttons there are
	private int activatedButtons = 0;

	// all the buttons in the group
	protected ArrayList buttonsInGroup = new ArrayList();

	// Grabs all the buttons in the group and stores them
	void Start ()
	{
		foreach(GameObject possiblePieces in GameObject.FindGameObjectsWithTag("Button")) {
			WeightedButton possiblePiece = possiblePieces.GetComponent<WeightedButton>();

			if(possiblePiece != null && possiblePiece.buttonGroup == buttonGroup) {
				buttonsInGroup.Add(possiblePiece);
				possiblePiece.ButtonController = this;
			}
		}
	}

	// checks if the button has been activated or deactivated
	public void buttonHasBeenManipulated(bool activated) {
		if(activated) {
			activatedButtons++;

			if(activatedButtons >= buttonsInGroup.Count) {
				print("Item has been activated");
			}

		} else {
			activatedButtons--;
			print("Item has been deactivated");
		}
	}
}

