using UnityEngine;
using System.Collections;

public class TalkingMinion : TalkingNpc
{
	public TalkingMinion() {
		npcDialogue.Add(0, "Hey there!");
		npcDialogue.Add(1, "I am a minion!");
		npcDialogue.Add(2, "Pretty cool huh?");
	}
}

