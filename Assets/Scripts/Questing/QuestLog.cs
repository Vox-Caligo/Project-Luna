using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    private QuestDB questDatabase;
    private QuestLogUI questLogUI;
    private CanvasGroup questLogUIGroup;

    public QuestLog() {
        GameObject questUI = Instantiate(Resources.Load("UI/Quest Log")) as GameObject;
        questLogUI = questUI.GetComponent<QuestLogUI>();
        questLogUIGroup = questUI.GetComponent<CanvasGroup>();
        questDatabase = GameObject.Find("Databases").GetComponent<QuestDB>();
    }

    // updates a quest and adds the quest line to be active if it is not already
    public void updateQuest(string questName, bool activation = false) {
        // gets the quest line with the active quest
        QuestLine activeQuestLine = questDatabase.getQuestline(questName);

        if (!activeQuestLine.questHasBeenCompleted(questName) && activeQuestLine.questPrereqsComplete(questName)) {
            print("Updating " + questName);

            if (!activation) {
                activeQuestLine.updateQuest(questName);
            }

            questLogUI.updateUI(activeQuestLine.getQuest(questName));

            if (activeQuestLine.getQuest(questName).QuestCompleted) {
                foreach (Quest activeQuest in activeQuestLine.ActiveQuests) {
                    questLogUI.updateUI(activeQuest);
                }
            }
        }
    }

    // removes an enemy amount from kill quest
    public void decrementFromKillQuest(string questName) {
        questDatabase.getQuestline(questName).decrementFromKillQuest(questName);
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

