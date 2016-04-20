using UnityEngine;
using System.Collections;

/**
 * Controlls all on/off switches for a given group and, when
 * all are correct, is responsible for an action.
 */
public class OnOffPuzzleController : LeverPuzzleController {
	// how many pieces are in the correct position
	private int numberCurrentlyCorrect = 0;

	// Grabs all puzzle pieces that belong to this controller
	void Start () {
		foreach(GameObject possiblePieces in GameObject.FindGameObjectsWithTag("Puzzle Piece")) {
			OnOffPuzzlePiece possiblePiece = possiblePieces.GetComponent<OnOffPuzzlePiece>();

			if(possiblePiece != null && possiblePiece.PuzzleGroup == puzzleGroup) {
				puzzlePiecesOfGroup.Add(possiblePiece);
				numberOfPieces++;
				if(possiblePiece.checkIfAlreadyCorrect()) {
					numberCurrentlyCorrect++;
				}
			}
		}
	}

	// adds/removes a piece to those currently correct
	public void changeNumberCurrentlyCorrect(bool isCorrect) {
		if(isCorrect) {
			numberCurrentlyCorrect++;
		} else {
			numberCurrentlyCorrect--;	
		}
	}

	// checks if all pieces are correctly activated and
	// does something as a result
	public override void checkIfSolved() {
		if(numberCurrentlyCorrect == numberOfPieces) {
			base.checkIfSolved();
		}
	}
}
