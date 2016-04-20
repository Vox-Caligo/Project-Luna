using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Controlls the dialogue when in conversation
 */
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

	// initializes conversation variables
	protected virtual void Start() {
		// gets the dialogue UI
		dialogueGroup = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<CanvasGroup>();
		dialogueText = dialogueGroup.GetComponentInChildren<Text>();

		// gets the choices from the player
		playerChoices = GameObject.Find("Player Choices").GetComponent<CanvasGroup>();

		// gets information from the databases
		currentSpeaker = GameObject.Find ("Speaker").GetComponent<Image>();
		speakerDatabase = GameObject.Find ("Databases").GetComponent<SpeakerDB> ();
		keyChecker = GameObject.Find ("Databases").GetComponent<KeyboardInput> ();

		// set buttons for being able to select options
		dialogueOptionOne = GameObject.Find ("Option One");
		dialogueOptionTwo = GameObject.Find ("Option Two");
		dialogueOptionOne.GetComponent<Button> ().onClick.AddListener (() => {
			selectedChoice (0);
		});
		dialogueOptionTwo.GetComponent<Button> ().onClick.AddListener (() => {
			selectedChoice (2);
		});
	}

	// updates the currently shown dialogue
	protected virtual bool updateDialogue(string newDialoguePiece, bool skipDialogue) {
		// checks if there is more dialogue to be shown
		if(newDialogueRunner < newDialoguePiece.Length) {
			if (skipDialogue) {
				// places letters individually if not skipped
				for (int i = newDialogueRunner; i < newDialoguePiece.Length; i++) {
					dialogueText.text += newDialoguePiece [i];
				}
			} else {
				if (!dialogueTyperDelay) {
					// places all the dialogue on the page
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

	// options for the player that can be selected
	protected virtual void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	// gets the choice that is currently selected
	protected virtual void selectedChoice(int buttonPressed) {}

	// timer (to be replaced)
	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}

	// checks if the talking point was complete
	public bool CompletedTalkingPoint {
		get { return completedTalkingPoint; }
	}
}