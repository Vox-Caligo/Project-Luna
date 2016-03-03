using UnityEngine;
using System.Collections;

public class InOrderPuzzlePiece : LeverPuzzlePiece {

	// Use this for initialization
	void Start () {
		foreach(GameObject newPuzzleController in GameObject.FindGameObjectsWithTag("Puzzle Controller")) {
			InOrderPuzzleController possibleController = newPuzzleController.GetComponent<InOrderPuzzleController>();

			if(possibleController != null && possibleController.puzzleGroup == puzzleGroup) {
				puzzleController = possibleController;
				break;
			}
		}
	}

	public override void onInteraction() {
		if(!deactivated) {
			((InOrderPuzzleController)puzzleController).addToPiecesCollected(this.pieceId);
			base.onInteraction();
		}
	}
}
