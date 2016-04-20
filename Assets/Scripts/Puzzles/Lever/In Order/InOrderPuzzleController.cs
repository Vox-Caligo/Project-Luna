using UnityEngine;
using System.Collections;

/**
 * A controller for a puzzle that needs to be activated
 * in a particular order
 */
public class InOrderPuzzleController : LeverPuzzleController
{
	// what current piece is being checked
	private int puzzlePieceRunner = 0;

	// finds all puzzle pieces for this group
	void Start() {
		foreach(GameObject possiblePieces in GameObject.FindGameObjectsWithTag("Puzzle Piece")) {
			InOrderPuzzlePiece possiblePiece = possiblePieces.GetComponent<InOrderPuzzlePiece>();

			if(possiblePiece != null && possiblePiece.PuzzleGroup == puzzleGroup) {
				puzzlePiecesOfGroup.Add(possiblePiece);
				numberOfPieces += possiblePiece.countOfWhenToHitInOrder();
			}
		}
	}

	// checks if the piece was correctly chosen and adds/resets
	// based on if it is/is not
	public bool addToPiecesCollected(int pieceId) {
		if(pieceId == puzzlePieceRunner) {
			puzzlePieceRunner++;
			return true;
		} else {
			puzzlePieceRunner = 0;	
			print("Incorrect");	// make an indication that it's wrong
			return false;
		}
	}

	// if all puzzle pieces have been activated, do something
	public override void checkIfSolved() {
		if(puzzlePieceRunner == numberOfPieces) {
			print("Yay, solved!");	// success (be sure to tag all puzzle pieces)
			base.checkIfSolved();
		}
	}
}