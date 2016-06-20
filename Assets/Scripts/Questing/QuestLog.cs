﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    private QuestDB questDatabase;
    private QuestLogUI questLogUI;
    private CanvasGroup questLogUIGroup;
    private Dictionary<string, bool> activeQuestLines = new Dictionary<string, bool>();

    public QuestLog() {
        GameObject questUI = Instantiate(Resources.Load("UI/Quest Log")) as GameObject;
        questLogUI = questUI.GetComponent<QuestLogUI>();
        questLogUIGroup = questUI.GetComponent<CanvasGroup>();
        questDatabase = GameObject.Find("Databases").GetComponent<QuestDB>();
    }

    // adds a quest that has been started to the log
    public void addActiveQuestLine(string questName) {
        if(!activeQuestLines.ContainsKey(questName)) {
            print("Added Quest Line: " + questName);
            activeQuestLines.Add(questName, false);
            questLogUI.addQuest(questName, "testing");
            // for this, find which questline it is and the quest name
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

    // turns the visibility of the UI on/off
    public void changeQuestLogVisibility() {
        questLogUI.Invisible = !questLogUI.Invisible;

        if (questLogUI.Invisible) {
            questLogUIGroup.alpha = 0;
        }
        else {
            questLogUIGroup.alpha = 1;
        }
    }
}

