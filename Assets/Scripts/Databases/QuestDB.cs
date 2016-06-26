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
        // test for kill quests
		allQuestlines.Add ("Kill the Minions", new QuestLine(new ArrayList {
            new Quest(0, "Kill the Villagers", "Exactly that!", 2, false),
            new Quest(1, "Kill the Minions", "Exactly that!", 2, false, null, new int[] { 0 }),
            new Quest(2, "Kill the Master Minion", "Exactly that!", 1, true, new int[] {0, 1})
        }, true));

        // test for location quests
        allQuestlines.Add("Go to Locations", new QuestLine(new ArrayList {
            new Quest(0, "Go to the Mana Spot", "Go to the Mana Spot"),
            new Quest(1, "Go to the Blood Pool", "Go to the Blood Pool", true, null, new int[] {0}),
            new Quest(2, "Go to the Health Generator", "Go to the Health Generator", true, new int[] {1}, new int[] {0})
        }, true));

        // test for item quests
        allQuestlines.Add("Deposit the Items", new QuestLine(new ArrayList {
            new Quest(0, "Deposit the Sword and Axe", "I need the sword please!")
        }, true));

        /*
        // test the entire questline
        allQuestlines.Add("Do all the Quests", new QuestLine("Do all the Quests", new ArrayList {
            new Quest(0, "Deposit the Sword and Axe", "I need the sword please!"),
            new Quest(1, "Kill the Minions", "Exactly that!", 2, false, null, new int[] {0}),
            new Quest(2, "Go to the Mana Spot", "Go to the Mana Spot", false, new int[] {1})
        }, true));
        */
    }

    // returns a quest line that contains a given quest
    public QuestLine getQuestline(string questName) {
        foreach(QuestLine questline in allQuestlines.Values) {
            if(questline.getQuest(questName) != null) {
                return questline;
            }
        }

        return null;
    }
}