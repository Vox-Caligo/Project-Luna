using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Controls a group of levers for a puzzle
 */
public class LeverPuzzleController : MonoBehaviour
{
	// the group that this controller is in charge of
	public int puzzleGroup = 0;

	// the number of pieces being controller
	protected int numberOfPieces = 0;

	// all the puzzle pieces in the group
	protected ArrayList puzzlePiecesOfGroup = new ArrayList();

	// check if the levers have been activated in the correct order
	public virtual void checkIfSolved() {	
		foreach(LeverPuzzlePiece puzzlePiece in puzzlePiecesOfGroup) {
			puzzlePiece.Deactivated = true;
		}
	}
}

