using UnityEngine;
using System.Collections;

public class KillQuestComponent : QuestComponentTemplate
{
    public bool questRequired;      // this character only is around when a quest is active
	private DefaultAI npcToKill;    // the character that the npc is on

	// applied to an npc that can be killed
	protected override void Start () {
        base.Start();
		npcToKill = this.gameObject.GetComponent<DefaultAI> ();
	}
}

