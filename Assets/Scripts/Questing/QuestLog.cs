using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    private QuestLogUI questLogUI;
    private Dictionary<string, bool> activeQuests = new Dictionary<string, bool>();

    public QuestLog() {
        questLogUI = new QuestLogUI();
    }

    // adds a quest that has been started to the log
    public void addActiveQuest(string questName) {
        if(!activeQuests.ContainsKey(questName)) {
            activeQuests.Add(questName, false);
        }
    }

    // checks if the quest is currently active, otherwise sets it so
    public void verifyActiveQuest(string questName) {
        if (!activeQuests.ContainsKey(questName)) {
            addActiveQuest(questName);
        }
    }

    // gets the current status of a quest in the log
    public bool getQuestStatus(string questName) {
        if (activeQuests.ContainsKey(questName)) {
            return activeQuests[questName];
        }

        return false;
    }

    // complete a quest
    public void completeQuest(string questName) {
        activeQuests[questName] = true;
    }

	public void updateQuestLog() {
		// foreach(string quest
		// find components
		// update them
	}
}

