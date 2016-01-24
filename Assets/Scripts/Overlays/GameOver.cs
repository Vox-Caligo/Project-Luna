﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
	private CanvasGroup myCanvas;
	
	public Button replayBtn;
	public Button quitBtn;
	public bool isShown = false;
	
	void Start () {
		myCanvas = gameObject.GetComponentInChildren<CanvasGroup>();
		replayBtn.onClick.AddListener (()=>{replayFromCheckpoint();});
		quitBtn.onClick.AddListener (()=>{quitGame();});
	}
	
	private void replayFromCheckpoint() {
		changeVisibility(false);
	}
	
	private void quitGame() {
		Application.Quit();
		//UnityEditor.EditorApplication.isPlaying = false;
	}

	// changes the interactability and visibility of the game
	public void changeVisibility(bool makeViewable) { 
		if(makeViewable) {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1;
			gameObject.GetComponentInChildren<CanvasGroup>().interactable = true;
			gameObject.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
			isShown = true;
		} else {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 0;
			gameObject.GetComponentInChildren<CanvasGroup>().interactable = false;
			gameObject.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
			isShown = false;
		}
	}
}
