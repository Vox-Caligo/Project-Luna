using UnityEngine;
using System.Collections;

/**
 * A puzzle piece that has to be hit (as a group)
 * in a certain order
 */
public class InOrderPuzzlePiece : LeverPuzzlePiece {
	// the order to be hit in
	public int[] whenToHitInOrder;

	// how many pieces have been activated currently
	private int numberOfActivations = 0;

	// Finds the puzzle controller for this piece
	void Start () {
		foreach(GameObject newPuzzleController in GameObject.FindGameObjectsWithTag("Puzzle Controller")) {
			InOrderPuzzleController possibleController = newPuzzleController.GetComponent<InOrderPuzzleController>();

			if(possibleController != null && possibleController.puzzleGroup == puzzleGroup) {
				puzzleController = possibleController;
				break;
			}
		}
	}

	// checks if the group is active and then either adds to number of activations (if correct)
	// or else resets all the numbers to zero
	public override void onInteraction() {
		if(!deactivated) {
			bool resetActivations;

			if (numberOfActivations < whenToHitInOrder.Length) {
				resetActivations = ((InOrderPuzzleController)puzzleController).addToPiecesCollected((int)whenToHitInOrder [numberOfActivations]);
			} else {
				resetActivations = ((InOrderPuzzleController)puzzleController).addToPiecesCollected (-1);
			}

			if (resetActivations) {
				numberOfActivations++;
			} else {
				numberOfActivations = 0;
			}

			base.onInteraction();
		}
	}

	// the order to hit them in
	public int countOfWhenToHitInOrder() {
		return whenToHitInOrder.Length;
	}
}
