using UnityEngine;
using System.Collections;

public class OnOffPuzzlePiece : LeverPuzzlePiece {
	public int pieceId = 0;
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

	public bool checkIfAlreadyCorrect() {
		return isOn == shouldBeOn;
	}
	
	public override void onInteraction() {
		if(!deactivated) {
			isOn = !isOn;

			if(isOn == shouldBeOn) {
				print("Right");
				((OnOffPuzzleController)puzzleController).changeNumberCurrentlyCorrect(true);
			} else {
				print("Wrong");
				((OnOffPuzzleController)puzzleController).changeNumberCurrentlyCorrect(false);
			}

			base.onInteraction();
		}
	}
}
