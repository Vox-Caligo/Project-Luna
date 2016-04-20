using UnityEngine;
using System.Collections;

/**
 * (Needs to be on something) A piece that can be turned
 * on and off, with one or the other being it's correct position.
 * It communicates with it's puzzle controller to see if it is 
 * completed.
 */
public class OnOffPuzzlePiece : LeverPuzzlePiece {
	// the number of the piece
	public int pieceId = 0;

	// bools for its current/intended status
	public bool isOn = false;
	public bool shouldBeOn = false;

	// Use this for initialization
	void Start () {
		foreach(GameObject newPuzzleController in GameObject.FindGameObjectsWithTag("Puzzle Controller")) {
			OnOffPuzzleController possibleController = newPuzzleController.GetComponent<OnOffPuzzleController>();

			if(possibleController != null && possibleController.puzzleGroup == puzzleGroup) {
				puzzleController = possibleController;
				break;
			}
		}
	}

	// checks if the switch is correctly switched
	public bool checkIfAlreadyCorrect() {
		return isOn == shouldBeOn;
	}

	// on interaction it turns on/off the object and, depending on which
	// it should be, tells the controller either it is correct or wrong.
	public override void onInteraction() {
		if(!deactivated) {
			isOn = !isOn;

			if(isOn == shouldBeOn) {
				((OnOffPuzzleController)puzzleController).changeNumberCurrentlyCorrect(true);
			} else {
				((OnOffPuzzleController)puzzleController).changeNumberCurrentlyCorrect(false);
			}

			base.onInteraction();
		}
	}
}
