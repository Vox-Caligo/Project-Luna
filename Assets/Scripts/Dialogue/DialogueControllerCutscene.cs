using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Controlls the dialogue being displayed during a cutscene
 */
public class DialogueControllerCutscene : DialogueController
{
	// Utilities
	private bool inCutscene = false;

	// Current Conversation
	private TalkingCharacterInformation currentCutsceneDialogue;

	protected virtual void Start() {
		base.Start ();
	}

	// Update is called once per frame
	private void FixedUpdate () {
		if (inCutscene) {
			if(!completedTalkingPoint) {
				completedTalkingPoint = updateDialogue (currentCutsceneDialogue.CharacterChat, keyChecker.useKey (KeyCode.E));
			} else {
				if (keyChecker.useKey (KeyCode.E)) {
					newDialogueRunner = 0;
					dialogueText.text = "";
					completedTalkingPoint = false;
				}
			}
		}
	}

	// Used for displaying dialogue within a cutscene, not standard npc interactions
	public void displayCutsceneDialogue(TalkingCharacterInformation currentCutsceneDialogue) {
		this.currentCutsceneDialogue = currentCutsceneDialogue;
		dialogueGroup.alpha = 1;
		inCutscene = true;
		currentSpeaker.sprite = Resources.Load (speakerDatabase.getSpeaker (currentCutsceneDialogue.Character), typeof(Sprite)) as Sprite;
	}

	// ends the current line being spoken
	public void endCurrentDialogue() {
		newDialogueRunner = 0;
		dialogueText.text = "";
		completedTalkingPoint = false;
	}

	// ends the dialogue that is being shown 
	public void endCutsceneDialogue() {
		this.currentCutsceneDialogue = null;
		dialogueGroup.alpha = 0;
		inCutscene = false;
	}

	// displays options that are given to the player to select from
	protected override void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	// returns the choice that was selected
	protected override void selectedChoice(int buttonPressed) {
		playerChoices.alpha = 0;
		//conversationNpc.CurrentDialogueSection = (int)conversationNpc.getCurrentOptions () [buttonPressed + 1];
	}
}

