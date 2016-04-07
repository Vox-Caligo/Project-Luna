using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
	// Dialogue UI
	protected Text dialogueText;
	protected CanvasGroup dialogueGroup;
	protected CanvasGroup playerChoices;
	protected GameObject dialogueOptionOne;
	protected GameObject dialogueOptionTwo;
	protected Image currentSpeaker;

	// Utilities
	protected int newDialogueRunner = 0;
	protected SpeakerDB speakerDatabase;
	protected KeyboardInput keyChecker;

	// Timer Properties
	protected float timerTick = 0;
	protected float maxTimer = .05f;
	protected bool dialogueTyperDelay = false;

	protected bool completedTalkingPoint = false;

	protected virtual void Start() {
		dialogueGroup = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<CanvasGroup>();
		dialogueText = dialogueGroup.GetComponentInChildren<Text>();
		playerChoices = GameObject.Find("Player Choices").GetComponent<CanvasGroup>();
		currentSpeaker = GameObject.Find ("Speaker").GetComponent<Image>();
		speakerDatabase = GameObject.Find ("Databases").GetComponent<SpeakerDB> ();
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();

		// set buttons
		dialogueOptionOne = GameObject.Find ("Option One");
		dialogueOptionTwo = GameObject.Find ("Option Two");
		dialogueOptionOne.GetComponent<Button> ().onClick.AddListener (() => {
			selectedChoice (0);
		});
		dialogueOptionTwo.GetComponent<Button> ().onClick.AddListener (() => {
			selectedChoice (2);
		});
	}

	protected virtual bool updateDialogue(string newDialoguePiece, bool skipDialogue) {
		if(newDialogueRunner < newDialoguePiece.Length) {
			if (skipDialogue) {
				for (int i = newDialogueRunner; i < newDialoguePiece.Length; i++) {
					dialogueText.text += newDialoguePiece [i];
				}
			} else {
				if (!dialogueTyperDelay) {
					dialogueText.text += newDialoguePiece [newDialogueRunner];
					newDialogueRunner++;
					dialogueTyperDelay = true;
				} else {
					if (timerCountdownIsZero ()) {
						dialogueTyperDelay = false;
						timerTick = maxTimer;
					}
				} 
				return false;
			}
		}
		return true;
	}

	protected virtual void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	protected virtual void selectedChoice(int buttonPressed) {}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}

	public bool CompletedTalkingPoint {
		get { return completedTalkingPoint; }
	}
}