using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
	// Dialogue UI
	private Text dialogueText;
	private CanvasGroup dialogueGroup;
	private GameObject dialogueOptionOne;
	private GameObject dialogueOptionTwo;
	private Image currentSpeaker;

	// Utilities
	private int newDialogueRunner = 0;
	private bool inConversation = false;
	private bool completedTalkingPoint = false;
	private bool interactButtonPressed = false;
	private SpeakerDB speakerDatabase;

	// Current Conversation
	protected TalkingNpc conversationNpc;
	protected Dictionary<int, string> npcDialogue;
	protected Dictionary<int, ArrayList> playerDialogue;
	protected ArrayList involvedActors;

	// Timer Properties
	private float timerTick = 0;
	private float maxTimer = .05f;
	private bool dialogueTyperDelay = false;

	void Start() {
		dialogueGroup = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<CanvasGroup>();
		dialogueText = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponentInChildren<Text>();
		dialogueOptionOne = GameObject.Find ("Option One");
		dialogueOptionTwo = GameObject.Find ("Option Two");
		currentSpeaker = GameObject.Find ("Speaker").GetComponent<Image>();
		speakerDatabase = GameObject.Find ("Databases").GetComponent<SpeakerDB> ();
	}

	// Update is called once per frame
	private void FixedUpdate () {
		if(inConversation) {
			if (!completedTalkingPoint) {
				if (Input.GetKeyDown (KeyCode.E) && !interactButtonPressed) {
					interactButtonPressed = true;
					haveConversation (true);
				} else {
					haveConversation (false);
				}
			} else {
				if(Input.GetKeyDown (KeyCode.E) && !interactButtonPressed) {
					interactButtonPressed = true;
					conversationNpc.CurrentDialogueSection = conversationNpc.CurrentDialogueSection + 1;
					newDialogueRunner = 0;
					dialogueText.text = "";
					completedTalkingPoint = false;
				}
			}
		}

		if (Input.GetKeyUp (KeyCode.E) && interactButtonPressed) {
			interactButtonPressed = false;
		}
	}

	public void enterConversation(TalkingNpc conversationNpc) {
		this.conversationNpc = conversationNpc;
		this.npcDialogue = conversationNpc.NpcDialogue;
		this.playerDialogue = conversationNpc.PlayerDialogue;
		this.involvedActors = conversationNpc.InvolvedActors;
		dialogueGroup.alpha = 1;
		inConversation = true;
		haveConversation(false);
	}

	private void endConversation() {
		this.npcDialogue = null;
		this.playerDialogue = null;
		dialogueGroup.alpha = 0;
		inConversation = false;
		conversationNpc.endConversation ();
	}

	private void haveConversation(bool skipDialogue) {
		if (npcDialogue.ContainsKey (conversationNpc.CurrentDialogueSection) || playerDialogue.ContainsKey (conversationNpc.CurrentDialogueSection)) {
			currentSpeaker.sprite = Resources.Load (speakerDatabase.getSpeaker (involvedActors [conversationNpc.CurrentDialogueSection].ToString()), typeof(Sprite)) as Sprite;

			if (npcDialogue.ContainsKey (conversationNpc.CurrentDialogueSection)) {
				completedTalkingPoint = updateDialogue (npcDialogue [conversationNpc.CurrentDialogueSection], skipDialogue);
			} else if (playerDialogue.ContainsKey (conversationNpc.CurrentDialogueSection)) {
				completedTalkingPoint = updateDialogue (playerDialogue [conversationNpc.CurrentDialogueSection] [0].ToString (), skipDialogue);
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

