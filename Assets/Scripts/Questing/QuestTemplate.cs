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
    
    

    protected void locateQuestLog() {
        questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
    }
}