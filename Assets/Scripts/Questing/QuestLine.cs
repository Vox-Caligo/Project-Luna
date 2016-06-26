using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for quests
 */

// Used to have multiple quests in one larger quest line
public class QuestLine : MonoBehaviour {
    private ArrayList completedQuestNumbers = new ArrayList();
    private ArrayList activeQuests;
    private ArrayList quests;

    private bool startableEarly;
    
    private string[] currentQuests;
    
    public QuestLine(ArrayList quests, bool startableEarly) {
        this.quests = quests;
        this.startableEarly = startableEarly;
    }

    public Quest getQuest(string questName) {
        foreach (Quest quest in quests) {
            if (quest.QuestName == questName) {
                return quest;
            }
        }
        return null;
    }

    public bool questHasBeenCompleted(string questName) {
        return completedQuestNumbers.Contains(getQuest(questName).QuestNumber);
    }

    public bool questPrereqsComplete(string questName) {
        Quest quest = getQuest(questName);

        int[] questReqs = quest.QuestsToBeDoneBefore;
        if (questReqs != null) {
            int[] questsToBeDone = quest.QuestsToBeDoneBefore;
            
            for(int i = 0; i < questsToBeDone.Length; i++) {
                if (!completedQuestNumbers.Contains(questsToBeDone[i])) {
                    return false;
                }
            }
            
            activeQuests.Add(quest);
        }

        return true;
    }

    // updates a quest in the quest line
    public void updateQuest(string questName) {
        Quest checkQuest = getQuest(questName);

        if (checkQuest.QuestName == questName) {
            checkQuest.updateQuest(completedQuestNumbers);
                
            if (checkQuest.QuestCompleted) {
                print("Completed the Quest: " + questName);
                completedQuestNumbers.Add(checkQuest.QuestNumber);

                int[] questsThatWillBeDone = checkQuest.QuestsThatWillBeDone;
                if (questsThatWillBeDone != null) {
                    foreach (int questNumber in questsThatWillBeDone) {
                        if (!completedQuestNumbers.Contains(questNumber)) {
                            completedQuestNumbers.Add(questNumber);
                        }
                    }
                }

                activatedQuests(checkQuest);
            }
        }
    }

    // decrements an enemy from the kill count requirement
    public void decrementFromKillQuest(string questName) {
        getQuest(questName).EnemyAmount--;
    }

    // activates quests that can be started after a quest has completed
    private void activatedQuests(Quest completedQuest) {
        activeQuests = new ArrayList();
        
        foreach (Quest quest in quests) {
            if (!quest.QuestCompleted && !completedQuestNumbers.Contains(quest.QuestNumber) && questPrereqsComplete(quest.QuestName)) {
                activeQuests.Add(quest);
            }
        }
    }

    public bool StartableEarly {
        get { return startableEarly; }
    }

    public ArrayList ActiveQuests {
        get { return activeQuests; }
    }
}