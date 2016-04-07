using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

	public void endCurrentDialogue() {
		newDialogueRunner = 0;
		dialogueText.text = "";
		completedTalkingPoint = false;
	}

	public void endCutsceneDialogue() {
		this.currentCutsceneDialogue = null;
		dialogueGroup.alpha = 0;
		inCutscene = false;
	}

	protected override void multiplePlayerOptions(ArrayList dialoguePieces) {
		playerChoices.alpha = 1;
		dialogueOptionOne.GetComponentInChildren<Text> ().text = dialoguePieces [0].ToString();
		dialogueOptionTwo.GetComponentInChildren<Text> ().text = dialoguePieces [2].ToString();
	}

	protected override void selectedChoice(int buttonPressed) {
		playerChoices.alpha = 0;
		//conversationNpc.CurrentDialogueSection = (int)conversationNpc.getCurrentOptions () [buttonPressed + 1];
	}

	protected bool timerCountdownIsZero() {
		if(timerTick > 0) {
			timerTick -= Time.deltaTime;
			return false;
		} else {
			return true;
		}
	}
}

