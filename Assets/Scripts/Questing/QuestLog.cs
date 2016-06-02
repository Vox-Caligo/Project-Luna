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
	
	// Update is called once per frame
	void Update ()
	{
	
	}

    public void addActiveQuest(string questName) {
        if(!activeQuests.ContainsKey(questName)) {
            activeQuests.Add(questName, false);
        }
    }

    public void completeActiveQuest(string questName) {
        if (activeQuests.ContainsKey(questName)) {
            activeQuests[questName] = true;
        }
    }

    public bool getQuestStatus(string questName) {
        if (activeQuests.ContainsKey(questName)) {
            return activeQuests[questName];
        }

        return false;
    }
}

