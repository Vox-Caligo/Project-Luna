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
    private int currentSection = 0;


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
            if (quest.QuestName == questName) {
                // if quest is startable early
                if(quest.QuestType == "kill") {
                    updateKillQuest(questName);
                } else if (quest.QuestType == "location") {

                }
                else if (quest.QuestType == "item") {

                }
            }
        }
    }

    // completes a kill quest that has been inside the log
    private void updateKillQuest(string questName) {
        print("Updated the Kill Quest: " + questName);
        questDatabase.decrementKillAmount(questLineName, currentSection);

        if (questDatabase.getKillAmount(questLineName, currentSection) == 0) {
            print("Completed the Kill Quest");
            completedQuestSection(questName);
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
    private void completedQuestSection(string questName) {
        if (currentSection < questDatabase.getQuestLength(questLineName)) {
            currentSection++;
        } else {
            questLog.completeQuest(questLineName);
        }
    }

    public string QuestLineName {
        get { return questLineName; }
    }

    public bool StartableEarly {
        get { return startableEarly; }
    }
}