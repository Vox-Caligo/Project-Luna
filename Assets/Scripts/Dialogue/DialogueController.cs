using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
	private Text dialogueText;
	private CanvasGroup dialogueGroup;
	private int newDialogueRunner = 0;
	private bool inConversation = false;
	private bool dialogueIsActivated = false;
	private bool completedTalkingPoint = false;
	protected int currentDialogueSection = 0;
	protected Dictionary<int, string> npcDialogue;
	protected Dictionary<int, ArrayList> playerDialogue;

	// timer properties
	private float timerTick = 0;
	private float maxTimer = .05f;
	private bool dialogueTyperDelay = false;

	void Start() {
		dialogueGroup = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponent<CanvasGroup>();
		dialogueText = GameObject.FindGameObjectWithTag("Dialogue Text").GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	private void FixedUpdate () {
		if(inConversation) {
			// in conversation and key is pressed (skip)
			if(Input.GetKeyUp(KeyCode.E) && dialogueIsActivated) {
				dialogueIsActivated = false;
			} else if(Input.GetKeyDown(KeyCode.E) && !dialogueIsActivated) {
				haveConversation();
				dialogueIsActivated = true;

				if(completedTalkingPoint) {
					currentDialogueSection++;
					newDialogueRunner = 0;
				} else {
					//print("STUFF STUFF STUFF");
				}
			} else if(dialogueIsActivated) {
				completedTalkingPoint = updateDialogue(npcDialogue[currentDialogueSection]);
			}
		}
	}

	public void enterConversation(Dictionary<int, string> npcDialogue, Dictionary<int, ArrayList> playerDialogue) {
		this.npcDialogue = npcDialogue;
		this.playerDialogue = playerDialogue;
		dialogueGroup.alpha = 1;
		inConversation = true;
		haveConversation();
	}

	private void haveConversation() {
		if(currentDialogueSection < npcDialogue.Count) {
			updateDialogue(npcDialogue[currentDialogueSection]);
			dialogueIsActivated = true;
		} else {
			inConversation = false;
		}
	}

	private bool updateDialogue(string newDialoguePiece) {
		if(newDialogueRunner < newDialoguePiece.Length) {
			if (!dialogueTyperDelay) {
				dialogueText.text += newDialoguePiece[newDialogueRunner];
				print("Weee");
				newDialogueRunner++;
				dialogueTyperDelay = true;
			} else {
				if(timerCountdownIsZero()) {
					dialogueTyperDelay = false;
					timerTick = maxTimer;
				}
			} 
			return false;
		} else {
			return true;
		}
	}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}

	public virtual void loopingSection() {
		currentDialogueSection = npcDialogue.Count - 1;
	}

	public bool InConversation {
		get {return inConversation; }
		set {inConversation = value; }
	}
}

