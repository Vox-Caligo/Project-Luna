using UnityEngine;
using System.Collections;

public class KillQuestComponent : QuestComponentTemplate
{
    public bool questRequired;      // this character only is around when a quest is active
	private DefaultAI npcToKill;    // the character that the npc is on
    private QuestLog questLog;

	// applied to an npc that can be killed
	protected void Start () {
		npcToKill = this.gameObject.GetComponent<DefaultAI> ();

        if(questRequired) {
            this.gameObject.SetActive(false);
        }
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

    void Update() {
        if(questRequired) {
            // check if quest is active and reenable
            // this.gameObject.SetActive(true);
        }
    }
}

