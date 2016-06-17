using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * (Put on Database GameObject) Holds every questline that may
 * be displayed in the visual inventory.
 */
public class QuestDB : MonoBehaviour {

	private Dictionary<string, QuestLine> allQuestlines = new Dictionary<string, QuestLine>();

    // sets the dictionary
    public QuestDB() {
        /*
        // test for kill quests
		allQuestlines.Add ("Kill the Minions", new QuestLine("Kill the Minions", new ArrayList {
            new Quest(0, "Kill the Minions", "Exactly that!", 2, false, null, new int[] {1}),
            new Quest(1, "Kill the Villagers", "Exactly that!", 2, false),
            new Quest(2, "Kill the Master Minion", "Exactly that!", 1, true, new int[] {0, 1 })
        }, true));

        // test for location quests
        allQuestlines.Add("Go to Locations", new QuestLine("Go to Locations", new ArrayList {
            new Quest(0, "Go to the Mana Spot", "Go to the Mana Spot"),
            new Quest(1, "Go to the Blood Pool", "Go to the Blood Pool", true, new int[] {0}),
            new Quest(2, "Go to the Health Generator", "Go to the Health Generator", true, null, new int[] {0})
        }, true));

        // test for item quests
        allQuestlines.Add("Deposit the Items", new QuestLine("Deposit the Items", new ArrayList {
            new Quest(0, "Deposit the Sword and Axe", "I need the sword please!")
        }, true));
        */

        // test the entire questline
        allQuestlines.Add("Do all the Quests", new QuestLine("Do all the Quests", new ArrayList {
            new Quest(0, "Deposit the Sword and Axe", "I need the sword please!"),
            new Quest(1, "Kill the Minions", "Exactly that!", 2, false, null, new int[] {0}),
            new Quest(2, "Go to the Mana Spot", "Go to the Mana Spot", false, new int[] {1})
        }, true));
    }

    // returns a value for the given item
    public string getValue(string currentQuest, int currentSection, string soughtValue) {
		if(allQuestlines.ContainsKey(currentQuest)) {
            Quest questToCheck = allQuestlines[currentQuest].getQuestWithIndex(currentSection);

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
    public int getKillAmount(string currentQuest, int questNumber) {
        if (allQuestlines.ContainsKey(currentQuest)) {
            Quest questToCheck = allQuestlines[currentQuest].getQuestWithIndex(questNumber);
            return questToCheck.EnemyAmount;
        }

        return -1;
    }

    // decrements a kill from a quest line if needed
    public void decrementKillAmount(string currentQuestLine, int questNumber) {
        if (allQuestlines.ContainsKey(currentQuestLine)) {
            Quest questToDecrement = allQuestlines[currentQuestLine].getQuestWithIndex(questNumber);
            questToDecrement.EnemyAmount -= 1;
        }
    }

    public bool questLineCanBeStartedEarly(string questLine) {
        return allQuestlines[questLine].StartableEarly;
    }

    public string questLineWithQuest(string questName) {
        foreach (QuestLine questLine in allQuestlines.Values) {
            if(questLine.containsQuest(questName)) {
                return questLine.QuestLineName;
            }
        }

        return "";
    }

    public void updateQuestLine(string questLine, string questName) {
        allQuestlines[questLine].updateQuest(questName);
    }
}