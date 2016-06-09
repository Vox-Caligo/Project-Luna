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
		allQuestlines.Add ("Kill the Minions", new QuestLine("Kill the Minions", new ArrayList {
            new Quest("Kill the Minions", "Kill the Minions", "Exactly that!", 2, false, false),
            new Quest("Kill the Master Minion", "Kill the Master Minion", "Exactly that!", 1, false, true)
        }, true));

        allQuestlines.Add("Go to the Mana Block", new QuestLine("Go to the Mana Block", new ArrayList {
            new Quest("Go to the Mana Block", "Get to the Mana Block", "Go there now", false, false)
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
    public int getKillAmount(string currentQuest, int currentSection) {
        if (allQuestlines.ContainsKey(currentQuest)) {
            Quest questToCheck = allQuestlines[currentQuest].getQuestWithIndex(currentSection);
            return questToCheck.EnemyAmount;
        }

        return -1;
    }

    // decrements a kill from a quest line if needed
    public void decrementKillAmount(string currentQuestLine, int currentSection) {
        if (allQuestlines.ContainsKey(currentQuestLine)) {
            Quest questToDecrement = allQuestlines[currentQuestLine].getQuestWithIndex(currentSection);
            questToDecrement.EnemyAmount -= 1;
        }
    }

    // gets the length of the entire questline
    public int getQuestLength(string currentQuestLine) {
        return allQuestlines[currentQuestLine].getQuestLineLength();
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