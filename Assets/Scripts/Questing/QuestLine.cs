using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for quests
 */

// Used to have multiple quests in one larger quest line
public class QuestLine : MonoBehaviour {
    private QuestDB questDatabase;
    private QuestLog questLog;
    private bool startableEarly { get; set; }
    private string questLineName;
    private ArrayList quests;
    private int completedQuests = 0;
    private ArrayList completedQuestNumbers = new ArrayList();

    public QuestLine(string questLineName, ArrayList quests, bool startableEarly) {
        this.questLineName = questLineName;
        this.quests = quests;
        this.startableEarly = startableEarly;
    }

    public Quest getQuestWithIndex(int questIndex) {
        return (Quest)quests[questIndex];
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

    public void updateQuest(string questName) {
        print("Update quest line");

        if(questDatabase == null || questLog == null) {
            questDatabase = GameObject.Find("Databases").GetComponent<QuestDB>();
            questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
        }


        // switch this to a while loop
        foreach (Quest quest in quests) {
            if (quest.QuestName == questName && checkQuestsDoneBefore(quest)) {
                // if quest is startable early
                if(quest.QuestType == "kill") {
                    updateKillQuest(quest);
                } else if (quest.QuestType == "location") {

                }
                else if (quest.QuestType == "item") {

                }
            }
        }
    }

    // checks that all prerequisite quests are done beforehand
    private bool checkQuestsDoneBefore(Quest quest) {
        if(quest.QuestsToBeDoneBefore == null) {
            return true;
        } else {
            for(int i = 0; i < quest.QuestsToBeDoneBefore.Length; i++) {
                if(!completedQuestNumbers.Contains(quest.QuestsToBeDoneBefore[i])) {
                    return false;
                }
            }
            return true;
        }
    }


    // completes a kill quest that has been inside the log
    private void updateKillQuest(Quest quest) {
        print("Updated the Kill Quest: " + quest.QuestName);
        questDatabase.decrementKillAmount(questLineName, quest.QuestNumber);

        if (questDatabase.getKillAmount(questLineName, quest.QuestNumber) == 0) {
            completedQuest(quest);
        }

        // if it meets the criteria then end the quest
        // activeQuests[questName] = true;
    }

    /* completes a location quest that has been inside the log
    private void updateLocationQuest(string questName, int locationNumber) {
        print("Updated the Location Quest: " + questName);
        questLog.verifyActiveQuest(questName);

    }

    // completes an item quest that has been inside the log
    private void updateItemQuest(string questName) {
        print("Updated the Item Quest: " + questName);
        questLog.verifyActiveQuest(questName);
    }
    */

    // finish a section of the overall questline
    private void completedQuest(Quest quest) {
        print("Completed the Quest: " + quest.QuestName);
        completedQuestNumbers.Add(quest.QuestNumber);
        completedQuests++;

        // finishes any quests that may be obsolete by the current being completed
        if (quest.QuestsThatWillBeDone != null) {
            for (int i = 0; i < quest.QuestsThatWillBeDone.Length; i++) {
                int optionalQuestNumber = quest.QuestsThatWillBeDone[i];

                if (!completedQuestNumbers.Contains(optionalQuestNumber)) {
                    completedQuestNumbers.Add(optionalQuestNumber);
                    completedQuests++;
                }
            }
        }

        if (completedQuests >= quests.Count) {
            questLog.completeQuestLine(questLineName);
        }
    }

    public string QuestLineName {
        get { return questLineName; }
    }

    public bool StartableEarly {
        get { return startableEarly; }
    }
}