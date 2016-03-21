using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkingMinion : TalkingNpc
{
	public TalkingMinion() { }

	public override void setupConversation() { 
		npcDialogue = new Dictionary<int, string>();
		playerDialogue = new Dictionary<int, ArrayList>();
		involvedActors = new ArrayList ();
		playerOptions = new ArrayList ();

		if (timesTalkedTo == 0) {
			npcDialogue.Add(0, "Hey there! Look how much I can talk! It's amazing how much I have to say. Oh, what am I?");
			npcDialogue.Add(1, "I am a minion! Well...that's what I've been called so far. There's a lot left to do but the game is coming along.");
			npcDialogue.Add(2, "Pretty cool huh? Well, I'll talk to you later! Actually...I don't think you introduced yourself!");

			playerOptions.Add ("The murderer Jimmy Sixer!");
			//playerOptions.Add("The hero Aegis!");
			playerDialogue.Add (3, playerOptions);
			npcDialogue.Add(4, "That's really coo...wait what?");

			involvedActors.Add ("Minion");
			involvedActors.Add ("Minion");
			involvedActors.Add ("Minion");
			involvedActors.Add ("Aegis");
			involvedActors.Add ("Minion");
		} else if (timesTalkedTo == 1) {
			npcDialogue.Add(0, "That's...that's kind of scary...");

			playerOptions = new ArrayList ();
			playerOptions.Add ("...");
			playerDialogue.Add (1, playerOptions);
			involvedActors.Add ("Minion");
			involvedActors.Add ("Aegis");
		} else {
			npcDialogue.Add(0, "...ok then...well bye?");

			playerOptions = new ArrayList ();
			playerOptions.Add ("...yeah...bye...");
			playerDialogue.Add (1, playerOptions);
			involvedActors.Add ("Minion");
			involvedActors.Add ("Aegis");
		}
	}
}

