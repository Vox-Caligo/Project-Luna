using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * A test conversation with a minion
 */
public class TalkingMasterMinion : TalkingNpc
{
	public TalkingMasterMinion() { }

	// the conversation that is set. With each timesTalkedTo increase
	// new conversations appear
	public override void setupConversation() { 
		conversationDialogue = new Dictionary<int, TalkingCharacterInformation> ();

		if (timesTalkedTo == 0) {
			conversationDialogue.Add(0, new TalkingCharacterInformation("Minion", "Please leave me alone. I am sad and will stop causing needless suffering."));

			// player options
			playerOptions = new ArrayList ();
			playerOptions.Add ("Ok!");
			playerOptions.Add (2);
			playerOptions.Add("Never!");
			playerOptions.Add (6);
			conversationDialogue.Add(1, new TalkingCharacterInformation("Aegis", playerOptions));
            
            questsDialogueCompletes.Add(2, "Kill the Master Minion");
            conversationDialogue.Add(2, new TalkingCharacterInformation("Minion", "Thank you friend."));
        }
	}
}

