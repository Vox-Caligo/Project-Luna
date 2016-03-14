using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour
{
	private bool inConversation = false;
	private bool dialogueIsActivated = false;
	protected int currentDialogueSection = 0;
	protected Dictionary<int, string> npcDialogue;
	protected Dictionary<int, ArrayList> playerDialogue;

	// Update is called once per frame
	private void FixedUpdate () {
		if(inConversation) {
			if(Input.GetKeyUp(KeyCode.E) && dialogueIsActivated) {
				dialogueIsActivated = false;
			} else if(Input.GetKeyDown(KeyCode.E) && !dialogueIsActivated) {
				haveConversation();
				dialogueIsActivated = true;
			}
		}
	}

	public void enterConversation(Dictionary<int, string> npcDialogue, Dictionary<int, ArrayList> playerDialogue) {
		this.npcDialogue = npcDialogue;
		this.playerDialogue = playerDialogue;
		inConversation = true;
		haveConversation();
	}

	private void haveConversation() {
		if(currentDialogueSection < npcDialogue.Count) {
			print(npcDialogue[currentDialogueSection]);
			currentDialogueSection++;
			dialogueIsActivated = true;
		} else {
			inConversation = false;
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

