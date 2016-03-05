using UnityEngine;
using System.Collections;

public class InOrderPuzzleController : LeverPuzzleController
{
	private int puzzlePieceRunner = 0;

	void Start() {
		foreach(GameObject possiblePieces in GameObject.FindGameObjectsWithTag("Puzzle Piece")) {
			InOrderPuzzlePiece possiblePiece = possiblePieces.GetComponent<InOrderPuzzlePiece>();

			if(possiblePiece != null && possiblePiece.PuzzleGroup == puzzleGroup) {
				puzzlePiecesOfGroup.Add(possiblePiece);
				numberOfPieces += possiblePiece.countOfWhenToHitInOrder();
			}
		}
	}

	public bool addToPiecesCollected(int pieceId) {
		print ("Runner is at: " + puzzlePieceRunner + " and given piece is: " + pieceId);
		if(pieceId == puzzlePieceRunner) {
			puzzlePieceRunner++;
			print("Correct");
			return true;
		} else {
			puzzlePieceRunner = 0;	
			print("Incorrect");	// make an indication that it's wrong
			return false;
		}
	}

	public override void checkIfSolved() {
		if(puzzlePieceRunner == numberOfPieces) {
			print("Yay, solved!");	// success (be sure to tag all puzzle pieces)
			base.checkIfSolved();
		}
	}
}

