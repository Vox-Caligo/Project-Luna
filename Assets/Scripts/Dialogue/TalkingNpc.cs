using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkingNpc : InteractableItem
{
	protected DialogueController dialogueController;
	protected Dictionary<int, string> npcDialogue = new Dictionary<int, string>();
	protected Dictionary<int, ArrayList> playerDialogue = new Dictionary<int, ArrayList>();
	protected bool nextDialogue = true;

	public override void onInteraction () {	
		if(dialogueController == null) {
			dialogueController = GameObject.Find("Dialogue").GetComponent<DialogueController>();
		}

		if(!dialogueController.InConversation) {
			dialogueController.enterConversation(npcDialogue, playerDialogue);
		}
	}
}

