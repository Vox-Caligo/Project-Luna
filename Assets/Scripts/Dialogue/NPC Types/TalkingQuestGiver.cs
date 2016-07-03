using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * A test conversation with a minion
 */
public class TalkingQuestGiver : TalkingNpc
{
    protected Dictionary<int, string> questsFromDialogue = new Dictionary<int, string>();
    protected QuestLog questLog;

    // the conversation that is set. With each timesTalkedTo increase
    // new conversations appear
    public override void setupConversation() { 
		conversationDialogue = new Dictionary<int, TalkingCharacterInformation> ();
        questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
    }

    public void checkIfQuestActivated() {
        if (questsFromDialogue.ContainsKey(currentDialogueSection)) {
            questLog.updateQuest(questsFromDialogue[currentDialogueSection], true);
        }
    }

    public void checkIfQuestCompleted() {
        if (questsDialogueCompletes.ContainsKey(currentDialogueSection)) {
            questLog.decrementFromKillQuest(questsDialogueCompletes[currentDialogueSection]);
            questLog.updateQuest(questsDialogueCompletes[currentDialogueSection]);
        }
    }
}

