using UnityEngine;
using System.Collections;

public class LeverPuzzlePiece : InteractableItem
{
	public int puzzleGroup = 0;
	public int pieceId = 0;
	protected bool deactivated = false;
	protected LeverPuzzleController puzzleController;

	// if interacted with, call controller
	public override void onInteraction() {
		puzzleController.checkIfSolved();
	}

	public int PuzzleGroup {
		get { return puzzleGroup; }
	}

	public bool Deactivated {
		set { deactivated = value; }
	}
}

