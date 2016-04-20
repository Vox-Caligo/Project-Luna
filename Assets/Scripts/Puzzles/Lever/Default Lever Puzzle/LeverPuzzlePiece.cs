using UnityEngine;
using System.Collections;

/**
 * A lever that can be pulled
 */
public class LeverPuzzlePiece : InteractableItem
{
	// current puzzle group
	public int puzzleGroup = 0;

	// checks if the lever is deactivated
	protected bool deactivated = false;

	// the controller for this lever
	protected LeverPuzzleController puzzleController;

	// if interacted with, call controller
	public override void onInteraction() {
		puzzleController.checkIfSolved();
	}

	// gets the current puzzle group
	public int PuzzleGroup {
		get { return puzzleGroup; }
	}

	// sets the deactivated variable
	public bool Deactivated {
		set { deactivated = value; }
	}
}

