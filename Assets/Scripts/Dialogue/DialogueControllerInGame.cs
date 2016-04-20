using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Controlls dialogue that is being shown during gameplay
 */
public class DialogueControllerInGame : DialogueController
{
	// Utilities
	private bool inConversation = false;

	// Current Conversation
	protected TalkingNpc conversationNpc;
	protected Dictionary<int, TalkingCharacterInformation> conversationDialogue;

	protected virtual void Start() {
		base.Start ();
	}

	// Update is called once per frame
	private void FixedUpdate () {
		if (inConversation) {
			if (!completedTalkingPoint) {
				// has a conversation with the character
				if (keyChecker.useKey (KeyCode.E)) {
					haveConversation (true);
				} else {
					haveConversation (false);
				}
			} else {
				if (keyChecker.useKey (KeyCode.E)) {
					// goes through the dialogue
					conversationNpc.CurrentDialogueSection = conversationNpc.CurrentDialogueSection + 1;
					newDialogueRunner = 0;
					dialogueText.text = "";
					completedTalkingPoint = false;
				}
			}
		}
	}

	// starts a conversation between characters
	public void enterConversation(TalkingNpc conversationNpc) {
		this.conversationNpc = conversationNpc;
		this.conversationDialogue = conversationNpc.ConversationDialogue;
		dialogueGroup.alpha = 1;
		inConversation = true;
		haveConversation(false);
	}

	// ends a conversation between characters
	private void endConversation() {
		this.conversationDialogue = null;
		dialogueGroup.alpha = 0;
		inConversation = false;
		conversationNpc.endConversation ();
	}

	// has a conversation 
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

	// displays options for the player to select
	protected override void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	// the option that the player selected
	protected override void selectedChoice(int buttonPressed) {
		if (playerChoices.alpha == 1) {
			playerChoices.alpha = 0;
			conversationNpc.CurrentDialogueSection = (int)conversationNpc.getCurrentOptions () [buttonPressed + 1];
			haveConversation (false);
		}
	}

	// checks if the character is in a conversation
	public bool InConversation {
		get {return inConversation; }
		set {inConversation = value; }
	}
}

