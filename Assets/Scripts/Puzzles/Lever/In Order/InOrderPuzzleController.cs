using UnityEngine;
using System.Collections;

public class InOrderPuzzleController : LeverPuzzleController
{
	private ArrayList puzzlePiecesCollected = new ArrayList();

	void Start() {
		foreach(GameObject possiblePieces in GameObject.FindGameObjectsWithTag("Puzzle Piece")) {
			InOrderPuzzlePiece possiblePiece = possiblePieces.GetComponent<InOrderPuzzlePiece>();

			if(possiblePiece != null && possiblePiece.PuzzleGroup == puzzleGroup) {
				puzzlePiecesOfGroup.Add(possiblePiece);
				numberOfPieces++;
			}
		}
	}

	public void addToPiecesCollected(int pieceId) {
		if(pieceId == puzzlePiecesCollected.Count) {
			puzzlePiecesCollected.Add(pieceId);
			print("Correct");
		} else {
			puzzlePiecesCollected.Clear();	
			print("Incorrect");	// make an indication that it's wrong
		}
	}

	public override void checkIfSolved() {
		if(puzzlePiecesCollected.Count == numberOfPieces) {
			print("Yay, solved!");	// success (be sure to tag all puzzle pieces)
			base.checkIfSolved();
		}
	}
}

