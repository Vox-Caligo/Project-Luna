using UnityEngine;
using System.Collections;

public class InOrderPuzzlePiece : LeverPuzzlePiece {

	public int[] whenToHitInOrder;
	private int numberOfActivations = 0;

	// Use this for initialization
	void Start () {
		foreach(GameObject newPuzzleController in GameObject.FindGameObjectsWithTag("Puzzle Controller")) {
			InOrderPuzzleController possibleController = newPuzzleController.GetComponent<InOrderPuzzleController>();

			if(possibleController != null && possibleController.puzzleGroup == puzzleGroup) {
				puzzleController = possibleController;
				break;
			}
		}
	}

	public override void onInteraction() {
		if(!deactivated) {
			bool resetActivations;

			if (numberOfActivations < whenToHitInOrder.Length) {
				resetActivations =((InOrderPuzzleController)puzzleController).addToPiecesCollected((int)whenToHitInOrder [numberOfActivations]);
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

	public int countOfWhenToHitInOrder() {
		return whenToHitInOrder.Length;
	}
}
