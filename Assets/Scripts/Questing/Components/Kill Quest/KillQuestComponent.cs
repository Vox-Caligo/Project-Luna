using UnityEngine;
using System.Collections;

public class KillQuestComponent : QuestComponentTemplate
{
	private DefaultAI npcToKill;    // the character that the npc is on
    private QuestLog questLog;

	// applied to an npc that can be killed
	protected override void Start () {
		npcToKill = this.gameObject.GetComponent<DefaultAI> ();
        base.Start();
	}

    public void updateKillQuest() {
        findQuestLog();
        questLog.decrementFromKillQuest(questName);
        this.updateQuest();
    }

    private void findQuestLog() {
        if (questLog == null) {
            questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
        }
    }
}

