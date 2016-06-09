using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Basic quests
public class Quest {
    // base for all quests
    public string QuestType { get; set; }
    public string QuestName { get; set; }
    public string QuestDescription { get; set; }
    public bool ConcurrentQuest { get; set; }
    public bool NeedsToBeOrdered { get; set; }

    // used for item quests
    public string ItemName { get; set; }

    // used for kill quests
    public int EnemyAmount { get; set; }

    // item quest
    public Quest(string questLineName, string questName, string questDescription, string itemName, bool concurrent, bool needsToBeOrdered) {
        QuestType = "item";
        QuestName = questName;
        QuestDescription = questDescription;
        ItemName = itemName;
        ConcurrentQuest = concurrent;
        NeedsToBeOrdered = needsToBeOrdered;
    }

    // kill quest
    public Quest(string questLineName, string questName, string questDescription, int enemyAmount, bool concurrent, bool needsToBeOrdered) {
        QuestType = "kill";
        QuestName = questName;
        QuestDescription = questDescription;
        EnemyAmount = enemyAmount;
        ConcurrentQuest = concurrent;
        NeedsToBeOrdered = needsToBeOrdered;
    }

    // location quest
    public Quest(string questLineName, string questName, string questDescription, bool concurrent, bool needsToBeOrdered) {
        QuestType = "location";
        QuestName = questName;
        QuestDescription = questDescription;
        ConcurrentQuest = concurrent;
        NeedsToBeOrdered = needsToBeOrdered;
    }
}