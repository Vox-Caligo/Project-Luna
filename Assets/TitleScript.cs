using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScript : MonoBehaviour {
	private SaveWorld worldSaver;
	
	public Button newBtn;
	public Button continueBtn;
	public Button quitBtn;
	
	private SoundInterpreter mySounds;
	/*
	// Use this for initialization
	void Start () {
		worldSaver = new SaveWorld();

		newBtn.onClick.AddListener (()=>{newGame();});
		continueBtn.onClick.AddListener (()=>{continueGame();});
		quitBtn.onClick.AddListener (()=>{quitGame();});
		
		mySounds = new SoundInterpreter();
		mySounds.playMusic("test", true);
	}
	
	private void newGame() {
		worldSaver.newGame();
		mySounds.playMusic("Woosh", false);
		Application.LoadLevel(1);
		Instantiate(Resources.Load("Aegis"));
	}
	
	private void continueGame() {
		worldSaver.pullStats();
		Application.LoadLevel(DialogueLua.GetActorField("Player", "OnLevel").AsInt);
		Instantiate(Resources.Load("Aegis"));
	}
	
	private void quitGame() {
		Application.Quit();
		//UnityEditor.EditorApplication.isPlaying = false;
	}
	*/
}
