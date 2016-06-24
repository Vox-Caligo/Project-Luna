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
    public void updateQuest(string questName) {
        // gets the quest line with the active quest
        QuestLine activeQuestLine = questDatabase.getQuestline(questName);
        activeQuestLine.updateQuest(questName);
        checkQuestStatuses(activeQuestLine);
    }

    private void checkQuestStatuses(QuestLine updatedQuestLine) {
        // check if certain quests should be added
        questLogUI.addQuest(questName, questDatabase.getValue(questLine, questDatabase.questIndexInQuestLine(questName, questLine), "Description"));

        // check if certain quests should be removed
        questLogUI.expireQuest(questName);
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

