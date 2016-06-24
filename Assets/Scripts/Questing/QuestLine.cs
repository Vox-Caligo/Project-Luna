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
    private ArrayList activeQuests = new ArrayList();
    private ArrayList quests;

    private bool startableEarly;
    private string questLineName;
    
    private string[] currentQuests;
    
    public QuestLine(string questLineName, ArrayList quests, bool startableEarly) {
        this.questLineName = questLineName;
        this.quests = quests;
        this.startableEarly = startableEarly;
    }

    public Quest getQuestWithIndex(int questIndex) {
        return (Quest)quests[questIndex];
    }

    // get location of quest index from the quest line
    public int getQuestIndex(string questName) {
        foreach (Quest quest in quests) {
            if (quest.QuestName == questName) {
                return quests.IndexOf(quest);
            }
        }

        return -1;
    }

    public int getQuestLineLength() {
        return quests.Count - 1;
    }

    public bool containsQuest(string questName) {
        foreach(Quest quest in quests) {
            if(quest.QuestName == questName) {
                return true;
            }
        }
        return false;
    }

    // updates a quest in the quest line
    public void updateQuest(string questName) {
        bool foundQuest = false;
        int questIdx = 0;

        while(!foundQuest && questIdx < quests.Count - 1) {
            Quest checkQuest = (Quest)quests[questIdx];

            if (checkQuest.QuestName == questName) {
                foundQuest = true;
                checkQuest.updateQuest(completedQuestNumbers);
                
                if (checkQuest.QuestCompleted) {
                    print("Completed the Quest: " + questName);
                    activatedQuests(checkQuest.QuestNumber);
                }
            } else {
                questIdx++;
            }
        }
    }

    // activates quests that can be started after a quest has completed
    private void activatedQuests(int completedQuest) {
        foreach (Quest possiblyActiveQuest in quests) {
            if (completedQuest != possiblyActiveQuest.QuestNumber) {
                int[] questReqs = possiblyActiveQuest.QuestsToBeDoneBefore;

                if (System.Array.IndexOf(possiblyActiveQuest.QuestsToBeDoneBefore, completedQuest) > 0) {
                    activeQuests.Add(possiblyActiveQuest);
                }
            }
        }
    }
    
    public string QuestLineName {
        get { return questLineName; }
    }

    public bool StartableEarly {
        get { return startableEarly; }
    }
}