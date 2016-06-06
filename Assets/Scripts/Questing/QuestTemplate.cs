using UnityEngine;
using System.Collections;

public class QuestTemplate : MonoBehaviour {
    public ArrayList questComponents;
    private int currentSection = 0;

    private QuestDB questDatabase;
    private QuestLog questLog;

    protected string questName;
    
    // Use this for initialization
    public QuestTemplate(string questName, int currentSection) {
        questComponents = new ArrayList();
        questDatabase = GameObject.Find("Database").GetComponent<QuestDB>();
        this.questName = questName;
        this.currentSection = currentSection;
    }

    protected virtual void storeComponents() {
        
    }
    
    // completes a kill quest that has been inside the log
    public void updateKillQuest() {
        print("Updated the Kill Quest: " + questName);
        questLog.verifyActiveQuest(questName);
        questDatabase.decrementKillAmount(questName, currentSection);

        if (questDatabase.getKillAmount(questName, currentSection) == 0) {
            print("Completed the Kill Quest: " + questName);
            completedQuestSection();
        }

        // if it meets the criteria then end the quest
        // activeQuests[questName] = true;
    }

    // completes a location quest that has been inside the log
    public void updateLocationQuest(string questName, int locationNumber) {
        print("Updated the Location Quest: " + questName);
        questLog.verifyActiveQuest(questName);

    }

    // completes an item quest that has been inside the log
    public void updateItemQuest(string questName) {
        print("Updated the Item Quest: " + questName);
        questLog.verifyActiveQuest(questName);
    }

    // finish a section of the overall questline
    private void completedQuestSection() {
        if (currentSection < questDatabase.getQuestLength(questName)) {
            currentSection++;
        }
        else {
            questLog.completeQuest(questName);
        }
    }

    protected void locateQuestLog() {
        questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
    }
}