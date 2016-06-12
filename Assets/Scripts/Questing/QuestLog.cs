using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    private QuestDB questDatabase;
    private QuestLogUI questLogUI;
    private Dictionary<string, bool> activeQuestLines = new Dictionary<string, bool>();

    public QuestLog() {
        questLogUI = new QuestLogUI();
        questDatabase = GameObject.Find("Databases").GetComponent<QuestDB>();
    }

    // adds a quest that has been started to the log
    public void addActiveQuestLine(string questName) {
        if(!activeQuestLines.ContainsKey(questName)) {
            activeQuestLines.Add(questName, false);
        }
    }

    // checks if the quest is currently active, otherwise sets it so
    public void verifyActiveQuestLine(string questName) {
        if (!activeQuestLines.ContainsKey(questName)) {
            addActiveQuestLine(questName);
        }
    }

    // gets the current status of a quest in the log
    public bool getQuestLineStatus(string questName) {
        if (activeQuestLines.ContainsKey(questName)) {
            return activeQuestLines[questName];
        }

        return false;
    }

    // complete a quest
    public void completeQuestLine(string questName) {
        print("Completed the Quest Line: " + questName);
        activeQuestLines[questName] = true;
    }

    public void updateQuestLine(string questName) {
        // gets the quest line with the active quest
        string questLineWithQuest = questDatabase.questLineWithQuest(questName);

        // adds a quest that was started if it should
        if (questLineWithQuest != "" && !activeQuestLines.ContainsKey(questLineWithQuest) && questDatabase.questLineCanBeStartedEarly(questLineWithQuest)) {
            print("Added Quest Line: " + questLineWithQuest);
            addActiveQuestLine(questLineWithQuest);
        }

        if (activeQuestLines.ContainsKey(questLineWithQuest)) {
            questDatabase.updateQuestLine(questLineWithQuest, questName);
        }
    }


    public void updateQuestLog() {
		// foreach(string quest
		// find components
		// update them
	}
}

