using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkingNpc : InteractableItem
{
	protected DialogueController dialogueController;
	protected Dictionary<int, string> npcDialogue;
	protected Dictionary<int, ArrayList> playerDialogue;
	protected ArrayList involvedActors;
	protected ArrayList playerOptions;
	protected string playerChoice;
	protected bool nextDialogue = true;
	protected int currentDialogueSection = 0;
	protected int timesTalkedTo = 0;

	public override void onInteraction () {	
		if(dialogueController == null) {
			dialogueController = GameObject.Find("Dialogue Controller").GetComponent<DialogueController>();
		}

		if(!dialogueController.InConversation) {
			setupConversation ();
			dialogueController.enterConversation(this);
		}
	}

	public virtual void setupConversation() { }

	public void endConversation() {	
		currentDialogueSection = 0;
		timesTalkedTo++;
	}

	public Dictionary<int, string> NpcDialogue {
		get {return npcDialogue;}
	}

	public Dictionary<int, ArrayList> PlayerDialogue {
		get { return playerDialogue; }
	}

	public int CurrentDialogueSection {
		get { return currentDialogueSection; }
		set { currentDialogueSection = value; }
	}

	public ArrayList InvolvedActors {
		get { return involvedActors; }
	}
}

