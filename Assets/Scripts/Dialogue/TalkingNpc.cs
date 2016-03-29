using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkingCharacterInformation {
	public string Character { get; set; }
	public string CharacterChat { get; set; }
	public ArrayList PlayerOptions { get; set; }

	public TalkingCharacterInformation(string character, string characterChat) {
		this.Character = character;
		this.CharacterChat = characterChat;
	}

	public TalkingCharacterInformation(string character, ArrayList playerOptions) {
		this.Character = character;
		this.PlayerOptions = playerOptions;
	}
}

public class TalkingNpc : InteractableItem
{
	protected DialogueControllerInGame dialogueController;
	protected Dictionary<int, TalkingCharacterInformation> conversationDialogue;
	protected ArrayList playerOptions;
	protected string playerChoice;
	protected bool nextDialogue = true;
	protected int currentDialogueSection = 0;
	protected int timesTalkedTo = 0;

	public override void onInteraction () {	
		if(dialogueController == null) {
			dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueControllerInGame>();
		}

		if(!dialogueController.InConversation) {
			setupConversation ();
			dialogueController.enterConversation(this);
		}
	}

	protected void setLoopingDialogue(int lowValue, int highValue) {
		if (currentDialogueSection < highValue) {
			currentDialogueSection = lowValue;
		} else {
			currentDialogueSection = highValue;
		}
	}

	public virtual void setupConversation() { }

	public void endConversation() {	
		//currentDialogueSection = 0;
		timesTalkedTo++;
	}

	public Dictionary<int, TalkingCharacterInformation> ConversationDialogue {
		get {return conversationDialogue;}
	}

	public int CurrentDialogueSection {
		get { return currentDialogueSection; }
		set { currentDialogueSection = value; }
	}

	public string getCurrentActor() {
		return conversationDialogue [currentDialogueSection].Character;
	}

	public string getCurrentDialogue() {
		return conversationDialogue [currentDialogueSection].CharacterChat;
	}

	public ArrayList getCurrentOptions() {
		if (conversationDialogue [currentDialogueSection].PlayerOptions != null) {
			return conversationDialogue [currentDialogueSection].PlayerOptions;
		}

		return null;
	}
}

