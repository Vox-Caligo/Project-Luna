using UnityEngine;
using System.Collections;

public class LeverPuzzlePiece : InteractableItem
{
	public int puzzleGroup = 0;
	public int pieceId = 0;
	private LeverPuzzleController puzzleController;

	void Start ()
	{
		foreach(GameObject newPuzzleController in GameObject.FindGameObjectsWithTag("Puzzle Controller")) {
			LeverPuzzleController possibleController = newPuzzleController.GetComponent<LeverPuzzleController>();

			if(possibleController.leverPuzzleGroup == puzzleGroup) {
				puzzleController = possibleController;
				break;
			}
		}
	}

	// if interacted with, call controller
	public override void onInteraction() {
		print("Look at me interact!");
	}
}

