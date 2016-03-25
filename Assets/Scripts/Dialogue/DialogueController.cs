using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
	// Dialogue UI
	private Text dialogueText;
	private CanvasGroup dialogueGroup;
	private CanvasGroup playerChoices;
	private GameObject dialogueOptionOne;
	private GameObject dialogueOptionTwo;
	private Image currentSpeaker;

	// Utilities
	private int newDialogueRunner = 0;
	private bool inConversation = false;
	private bool completedTalkingPoint = false;
	private SpeakerDB speakerDatabase;
	private KeyboardInput keyChecker;

	// Current Conversation
	protected TalkingNpc conversationNpc;
	protected Dictionary<int, TalkingCharacterInformation> conversationDialogue;

	// Timer Properties
	private float timerTick = 0;
	private float maxTimer = .05f;
	private bool dialogueTyperDelay = false;

	void Start() {
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

	// Update is called once per frame
	private void FixedUpdate () {
		if(inConversation) {
			if (!completedTalkingPoint) {
				if (keyChecker.useKey(KeyCode.E)) {
					haveConversation (true);
				} else {
					haveConversation (false);
				}
			} else {
				if(keyChecker.useKey(KeyCode.E)) {
					conversationNpc.CurrentDialogueSection = conversationNpc.CurrentDialogueSection + 1;
					newDialogueRunner = 0;
					dialogueText.text = "";
					completedTalkingPoint = false;
				}
			}
		}
	}

	public void enterConversation(TalkingNpc conversationNpc) {
		this.conversationNpc = conversationNpc;
		this.conversationDialogue = conversationNpc.ConversationDialogue;
		dialogueGroup.alpha = 1;
		inConversation = true;
		haveConversation(false);
	}

	private void endConversation() {
		this.conversationDialogue = null;
		dialogueGroup.alpha = 0;
		inConversation = false;
		conversationNpc.endConversation ();
	}

	private void haveConversation(bool skipDialogue) {
		if (conversationDialogue.ContainsKey (conversationNpc.CurrentDialogueSection)) {
			currentSpeaker.sprite = Resources.Load (speakerDatabase.getSpeaker (conversationNpc.getCurrentActor()), typeof(Sprite)) as Sprite;

			if (conversationDialogue.ContainsKey (conversationNpc.CurrentDialogueSection)) {
				if (conversationNpc.getCurrentOptions() == null) {
					completedTalkingPoint = updateDialogue (conversationNpc.getCurrentDialogue(), skipDialogue);
				} else {
					multiplePlayerOptions (conversationNpc.getCurrentOptions());
				}
			} 
		} else {
			endConversation ();
		}
	}

	private bool updateDialogue(string newDialoguePiece, bool skipDialogue) {
		if(newDialogueRunner < newDialoguePiece.Length) {
			if (skipDialogue) {
				for (int i = newDialogueRunner; i < newDialoguePiece.Length; i++) {
					dialogueText.text += newDialoguePiece [i];
				}
				return true;
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

	private void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	private void selectedChoice(int buttonPressed) {
		playerChoices.alpha = 0;
		conversationNpc.CurrentDialogueSection = (int)conversationNpc.getCurrentOptions () [buttonPressed + 1];
		haveConversation (false);
	}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}

	public bool InConversation {
		get {return inConversation; }
		set {inConversation = value; }
	}
}