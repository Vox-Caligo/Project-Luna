using UnityEngine;
using System.Collections;

public class WeightedButtonController : MonoBehaviour
{
	public int buttonGroup;
	private int activatedButtons = 0;
	protected ArrayList buttonsInGroup = new ArrayList();

	// Use this for initialization
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

