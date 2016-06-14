using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * A test conversation with a minion
 */
public class TalkingQuestGiver : TalkingNpc
{
	public TalkingQuestGiver() { }

	// the conversation that is set. With each timesTalkedTo increase
	// new conversations appear
	public override void setupConversation() { 
		conversationDialogue = new Dictionary<int, TalkingCharacterInformation> ();

		if (timesTalkedTo == 0) {
			conversationDialogue.Add(0, new TalkingCharacterInformation("Minion", "Hey there! Can you do me a quick favor?"));
			conversationDialogue.Add(1, new TalkingCharacterInformation("Aegis", "Suuuure. What is it?"));
			conversationDialogue.Add(2, new TalkingCharacterInformation("Minion", "Murder everyone please ;)"));

			// player options
			playerOptions = new ArrayList ();
			playerOptions.Add ("Ok!");
			playerOptions.Add (4);
			playerOptions.Add("Never!");
			playerOptions.Add (6);
			conversationDialogue.Add(3, new TalkingCharacterInformation("Aegis", playerOptions));

			conversationDialogue.Add(4, new TalkingCharacterInformation("Minion", "Awesome!"));
            questsFromDialogue.Add(4, "Kill the Minions");
            
            conversationDialogue.Add(6, new TalkingCharacterInformation("Minion", "Then I'm going to kill you!"));
		}
	}
}

