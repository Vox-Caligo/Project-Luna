﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Information about the talking character including the character,
 * the chat dialogue, and player options that exist.
 */
public class TalkingCharacterInformation {
	public string Character { get; set; }
	public string CharacterChat { get; set; }
	public ArrayList PlayerOptions { get; set; }

	// sets dialogue that requires no action
	public TalkingCharacterInformation(string character, string characterChat) {
		this.Character = character;
		this.CharacterChat = characterChat;
	}

	// sets dialogue that requires player input
	public TalkingCharacterInformation(string character, ArrayList playerOptions) {
		this.Character = character;
		this.PlayerOptions = playerOptions;
	}
}

/**
 * An NPC that can have a conversation with the player
 */
public class TalkingNpc : InteractableItem
{
	// variables for controlling the flow of dialogue
	protected DialogueControllerInGame dialogueController;
	protected Dictionary<int, TalkingCharacterInformation> conversationDialogue;
    protected Dictionary<int, int> karmaFromDialogue = new Dictionary<int, int>();
    protected Dictionary<int, string> questsDialogueCompletes = new Dictionary<int, string>();

    // variables for player choices in conversation
    protected ArrayList playerOptions;
	protected string playerChoice;
    protected int playerKarma;

	// variables for dialogue traversal
	protected bool nextDialogue = true;
	protected int currentDialogueSection = 0;
	protected int timesTalkedTo = 0;

    // NPC AI
    protected DefaultAI npcAI;
    protected Dictionary<int, string> combatCommand = new Dictionary<int, string>();
    protected Dictionary<int, string> movementCommand = new Dictionary<int, string>();

	// initializes/continues a conversation with this NPC
	public override void onInteraction () {	
		if(dialogueController == null) {
			dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueControllerInGame>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //questLog = player.GetComponent<PlayerMaster>().PlayerQuests;
            playerKarma = player.GetComponent<PlayerMaster>().Karma;

            //if (conversationNpc.GetType().IsSubclassOf(typeof(TalkingQuestGiver))) {
            npcAI = this.gameObject.GetComponent<DefaultAI>();
        }

        npcAI.InConversation = true;

        if (!dialogueController.InConversation) {
			setupConversation ();
			dialogueController.enterConversation(this);
		}
	}

	// repeats the ending of dialogue so characters always have something to say
	protected void setLoopingDialogue(int lowValue, int highValue) {
		if (currentDialogueSection < highValue) {
			currentDialogueSection = lowValue;
		} else {
			currentDialogueSection = highValue;
		}
	}

	// sets up the current conversation
	public virtual void setupConversation() { }

	// ends the current conversation
	public void endConversation() {	
		timesTalkedTo++;
        npcAI.InConversation = false;
        print("Ended: " + movementCommand.ContainsKey(currentDialogueSection - 1));

        if(combatCommand.ContainsKey(currentDialogueSection - 1)) {
            npcAI.changeCombatCommand(combatCommand[currentDialogueSection]);
        }

        if(movementCommand.ContainsKey(currentDialogueSection - 1)) {
            npcAI.changeMovementCommand(movementCommand[currentDialogueSection - 1]);
        }
    }

	// a dictionary holding all the dialogue in a conversation
	public Dictionary<int, TalkingCharacterInformation> ConversationDialogue {
		get {return conversationDialogue;}
	}

	// gets the current dialogue being spoken
	public int CurrentDialogueSection {
		get { return currentDialogueSection; }
		set { currentDialogueSection = value; }
	}

	// gets the currently speaking character
	public string getCurrentActor() {
		return conversationDialogue [currentDialogueSection].Character;
	}

	// gets the currently spoken dialogue
	public string getCurrentDialogue() {
		return conversationDialogue [currentDialogueSection].CharacterChat;
	}

	// gets the current options that the player may choose
	public ArrayList getCurrentOptions() {
		if (conversationDialogue [currentDialogueSection].PlayerOptions != null) {
			return conversationDialogue [currentDialogueSection].PlayerOptions;
		}

		return null;
	}

    public void checkIfKarmaChange() {
        if (karmaFromDialogue.ContainsKey(currentDialogueSection)) {
            playerKarma = playerKarma + karmaFromDialogue[currentDialogueSection];
        }
    }
}

