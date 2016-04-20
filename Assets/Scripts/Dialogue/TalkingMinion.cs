using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * A test conversation with a minion
 */
public class TalkingMinion : TalkingNpc
{
	public TalkingMinion() { }

	// the conversation that is set. With each timesTalkedTo increase
	// new conversations appear
	public override void setupConversation() { 
		conversationDialogue = new Dictionary<int, TalkingCharacterInformation> ();

		if (timesTalkedTo == 0) {
			conversationDialogue.Add(0, new TalkingCharacterInformation("Minion", "Hey there! Look how much I can talk! It's amazing how much I have to say. Oh, what am I?"));
			conversationDialogue.Add(1, new TalkingCharacterInformation("Minion", "I am a minion! Well...that's what I've been called so far. There's a lot left to do but the game is coming along."));
			conversationDialogue.Add(2, new TalkingCharacterInformation("Minion", "Pretty cool huh? Well, I'll talk to you later! Actually...I don't think you introduced yourself!"));

			// player options
			playerOptions = new ArrayList ();
			playerOptions.Add ("The murderer Jimmy Sixer!");
			playerOptions.Add (4);
			playerOptions.Add("The hero Aegis!");
			playerOptions.Add (12);
			conversationDialogue.Add(3, new TalkingCharacterInformation("Aegis", playerOptions));

			conversationDialogue.Add(4, new TalkingCharacterInformation("Minion", "That's really coo...wait what?"));
			conversationDialogue.Add(5, new TalkingCharacterInformation("Aegis", "That's right and I'm here to kill you!"));
			conversationDialogue.Add(6, new TalkingCharacterInformation("Minion", "Nooooo!!!"));
			conversationDialogue.Add(7, new TalkingCharacterInformation("Aegis", "Or am I joking?"));

			conversationDialogue.Add(12, new TalkingCharacterInformation("Minion", "That's really cool!"));
			conversationDialogue.Add(13, new TalkingCharacterInformation("Aegis", "Thanks!"));
		} else if (timesTalkedTo == 1) {
			conversationDialogue.Add(8, new TalkingCharacterInformation("Minion", "That's...that's kind of scary..."));
			conversationDialogue.Add(9, new TalkingCharacterInformation("Aegis", "..."));

			conversationDialogue.Add(14, new TalkingCharacterInformation("Minion", "That's...that's kind of awesome..."));
			conversationDialogue.Add(15, new TalkingCharacterInformation("Aegis", "...yeeeeah"));
		} else {
			conversationDialogue.Add(10, new TalkingCharacterInformation("Minion", "...ok then...well bye?"));
			conversationDialogue.Add(11, new TalkingCharacterInformation("Aegis", "...yeah...bye..."));

			conversationDialogue.Add(16, new TalkingCharacterInformation("Minion", "STAY HERE FOREVER WITH ME!"));
			conversationDialogue.Add(17, new TalkingCharacterInformation("Aegis", "...yeah...no..."));

			setLoopingDialogue (10, 16);
		}
	}
}

