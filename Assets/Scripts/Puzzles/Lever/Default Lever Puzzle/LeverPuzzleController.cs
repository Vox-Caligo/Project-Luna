using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeverPuzzleController : MonoBehaviour
{
	public int puzzleGroup = 0;
	protected int numberOfPieces = 0;
	protected ArrayList puzzlePiecesOfGroup = new ArrayList();

	// check if the levers have been activated in the correct order
	public virtual void checkIfSolved() {	
		foreach(LeverPuzzlePiece puzzlePiece in puzzlePiecesOfGroup) {
			puzzlePiece.Deactivated = true;
		}
	}
}

