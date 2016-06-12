using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Basic quests
public class Quest {
    // base for all quests
    public int QuestNumber { get; set; }
    public string QuestType { get; set; }
    public string QuestName { get; set; }
    public string QuestDescription { get; set; }
    
    public bool IsMandatory { get; set; }
    public int[] QuestsToBeDoneBefore { get; set; }
    public int[] QuestsThatWillBeDone { get; set; }

    // used for item quests
    public string ItemName { get; set; }

    // used for kill quests
    public int EnemyAmount { get; set; }

    // item quest
    public Quest(int questNumber, string questName, string questDescription, string itemName, bool isMandatory = false, int[] questsToBeDoneBefore = null, int[] questsThatWillBeDone = null) {
        applyBasicQuestComponents(questNumber, "item", questName, questDescription, isMandatory, questsToBeDoneBefore, questsThatWillBeDone);
        ItemName = itemName;
    }

    // kill quest
    public Quest(int questNumber, string questName, string questDescription, int enemyAmount, bool isMandatory = false, int[] questsToBeDoneBefore = null, int[] questsThatWillBeDone = null) {
        applyBasicQuestComponents(questNumber, "kill", questName, questDescription, isMandatory, questsToBeDoneBefore, questsThatWillBeDone);
        EnemyAmount = enemyAmount;
    }

    // location quest
    public Quest(int questNumber, string questName, string questDescription, bool isMandatory = false, int[] questsToBeDoneBefore = null, int[] questsThatWillBeDone = null) {
        applyBasicQuestComponents(questNumber, "location", questName, questDescription, isMandatory, questsToBeDoneBefore, questsThatWillBeDone);
    }

    private void applyBasicQuestComponents(int questNumber, string questType, string questName, string questDescription, bool isMandatory, int[] questsToBeDoneBefore, int[] questsThatWillBeDone) {
        QuestNumber = questNumber;
        QuestType = questType;
        QuestName = questName;
        QuestDescription = questDescription;
        IsMandatory = isMandatory;
        QuestsToBeDoneBefore = questsToBeDoneBefore;
        QuestsThatWillBeDone = questsThatWillBeDone;
    }
}