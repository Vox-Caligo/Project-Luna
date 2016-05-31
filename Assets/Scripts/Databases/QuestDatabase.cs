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
	public string EnemyType { get; set; }
	public int EnemyAmount { get; set; }

	// used for location quests
	public Vector2 LocationCoords { get; set; }

	// item quest
	public Quest(string questName, string questDescription, string itemName) {
		QuestType = "item";
		QuestName = questName;
		QuestDescription = questDescription;
		ItemName = itemName;
	}

	// kill quest
	public Quest(string questName, string questDescription, string enemyType, int enemyAmount) {
		QuestType = "kill";
		QuestName = questName;
		QuestDescription = questDescription;
		EnemyType = enemyType;
		EnemyAmount = enemyAmount;
	}

	// location quest
	public Quest(string questName, string questDescription, Vector2 locationCoords) {
		QuestType = "location";
		QuestName = questName;
		QuestDescription = questDescription;
		LocationCoords = locationCoords;
	}
}

/**
 * (Put on Database GameObject) Holds every item that may
 * be displayed in the visual inventory. This includes the
 * name, image path, description, effects, and item type.
 */
public class QuestDatabase : MonoBehaviour {

	private Dictionary<string, Quest> allQuests;

	// sets the dictionary
	void Awake() {
		allQuests = new Dictionary<string, Quest> ();
		allQuests.Add ("Kill the Minions", new Quest("Kill the Minions", "Exactly that!", "Minion", 2));
	}

	// returns a value for the given item
	public string getValue(string currentQuest, string soughtValue) {
		if(allQuests.ContainsKey(currentQuest)) {
			switch (soughtValue) {
			case "Name":
				return allQuests[currentQuest].QuestName;
			case "Type":
				return allQuests[currentQuest].QuestType;
			case "Description":
				return allQuests[currentQuest].QuestDescription;
			case "Item":
				return allQuests[currentQuest].ItemName;
			case "Enemy":
				return allQuests[currentQuest].EnemyType;
			//case "Kill Amount":
				//return allQuests[currentQuest].EnemyAmount;
			//case "Location":
				//return allQuests[currentQuest].LocationCoords;
			}
		} 

		print("Quest item does not exist");
		return null;
	}
}