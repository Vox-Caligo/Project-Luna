using UnityEngine;
using System.Collections;

public class OnOffPuzzleController : LeverPuzzleController {
	private int numberCurrentlyCorrect = 0;

	// Use this for initialization
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
	
	public void changeNumberCurrentlyCorrect(bool isCorrect) {
		if(isCorrect) {
			numberCurrentlyCorrect++;
			print("Correct");
		} else {
			numberCurrentlyCorrect--;	
			print("Incorrect");	// make an indication that it's wrong
		}
	}

	public override void checkIfSolved() {
		if(numberCurrentlyCorrect == numberOfPieces) {
			print("Yay, solved!");	// success (be sure to tag all puzzle pieces)
			base.checkIfSolved();
		}
	}
}
