using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    private QuestDB questDatabase;
    private QuestLogUI questLogUI;
    private CanvasGroup questLogUIGroup;
    private PlayerMaster player;

    public QuestLog() {
        GameObject questUI = Instantiate(Resources.Load("UI/Quest Log")) as GameObject;
        questLogUI = questUI.GetComponent<QuestLogUI>();
        questLogUIGroup = questUI.GetComponent<CanvasGroup>();
        questDatabase = GameObject.Find("Databases").GetComponent<QuestDB>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>();
    }

    // updates a quest and adds the quest line to be active if it is not already
    public void updateQuest(string questName, bool activation = false) {
        // gets the quest line with the active quest
        QuestLine activeQuestLine = questDatabase.getQuestline(questName);
        Quest currentQuest = activeQuestLine.getQuest(questName);

        if (!currentQuest.QuestCompleted && activeQuestLine.questPrereqsComplete(currentQuest)) {
            print("Updating " + questName);

            if (!activation) {
                activeQuestLine.updateQuest(questName);
            }
        
            questLogUI.updateUI(currentQuest);

            if (currentQuest.QuestCompleted) {

                // retrive quest reward
                if (currentQuest.QuestRewards != null) {
                    giveQuestRewards(currentQuest.QuestRewards);
                }

                foreach (Quest activeQuest in activeQuestLine.ActiveQuests) {
                    questLogUI.updateUI(activeQuest);
                }
            }
        }
    }

    // removes an enemy amount from kill quest
    public void decrementFromKillQuest(string questName) {
        questDatabase.getQuestline(questName).decrementFromKillQuest(questName);
    }

    // turns the visibility of the UI on/off
    public void changeQuestLogVisibility() {
        questLogUI.Invisible = !questLogUI.Invisible;

        if (questLogUI.Invisible) {
            questLogUIGroup.alpha = 0;
            questLogUIGroup.blocksRaycasts = false;
        }
        else {
            questLogUIGroup.alpha = 1;
            questLogUIGroup.blocksRaycasts = true;
        }
    }

    private void giveQuestRewards(ArrayList rewards) {
        foreach (string reward in rewards) {
            string[] rewardItems = reward.Split('/');
            
            if (rewardItems[0] == "Karma") {
                int karmaReward = int.Parse(rewardItems[1]);
                player.Karma = player.Karma + karmaReward;
            } else if (rewardItems[0] == "Item") {
                player.PlayerInventory.addItemFromInventory(rewardItems[1], rewardItems[2]);
            }
        }
    }
}

