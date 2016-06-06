using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Easy database reading for items
 */
class Quest {
	// base for all quests
	public string QuestType { get; set; }
	public string QuestName { get; set; }
	public string QuestDescription { get; set; }

	// used for item quests
	public string ItemName { get; set; }

	// used for kill quests
	public int EnemyAmount { get; set; }

	// item quest
	public Quest(string questName, string questDescription, string itemName) {
		QuestType = "item";
		QuestName = questName;
		QuestDescription = questDescription;
		ItemName = itemName;
	}

	// kill quest
	public Quest(string questName, string questDescription, int enemyAmount) {
		QuestType = "kill";
		QuestName = questName;
		QuestDescription = questDescription;
		EnemyAmount = enemyAmount;
	}

	// location quest
	public Quest(string questName, string questDescription) {
		QuestType = "location";
		QuestName = questName;
		QuestDescription = questDescription;
	}
}

/**
 * (Put on Database GameObject) Holds every item that may
 * be displayed in the visual inventory. This includes the
 * name, image path, description, effects, and item type.
 */
public class QuestDB : MonoBehaviour {

	private Dictionary<string, ArrayList> allQuests;

	// sets the dictionary
	public QuestDB() {
		allQuests = new Dictionary<string, ArrayList> ();
		allQuests.Add ("Kill the Minions", new ArrayList { new Quest("Kill the Minions", "Exactly that!", 2) });
        allQuests.Add("Go to the Mana Block", new ArrayList { new Quest("Get to the Mana Block", "Go there now") });
	}

	// returns a value for the given item
	public string getValue(string currentQuest, int currentSection, string soughtValue) {
		if(allQuests.ContainsKey(currentQuest)) {
            Quest questToCheck = (Quest)allQuests[currentQuest][currentSection];

            switch (soughtValue) {
			case "Name":
				return questToCheck.QuestName;
			case "Type":
				return questToCheck.QuestType;
			case "Description":
				return questToCheck.QuestDescription;
			case "Item":
				return questToCheck.ItemName;
			}
		} 

		print("Quest item does not exist");
		return null;
	}

    // returns a value for the amount of kills needed
    public int getKillAmount(string currentQuest, int currentSection) {
        if (allQuests.ContainsKey(currentQuest)) {
            Quest questToCheck = (Quest)allQuests[currentQuest][currentSection];
            return questToCheck.EnemyAmount;
        }

        return -1;
    }

    // decrements a kill from a quest line if needed
    public void decrementKillAmount(string currentQuest, int currentSection) {
        if (allQuests.ContainsKey(currentQuest)) {
            Quest questToDecrement = (Quest)allQuests[currentQuest][currentSection];
            questToDecrement.EnemyAmount -= 1;
        }
    }

    // gets the length of the entire questline
    public int getQuestLength(string currentQuest) {
        return allQuests[currentQuest].Count - 1;
    }
}