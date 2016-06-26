using UnityEngine;
using System.Collections;

public class QuestComponentTemplate : MonoBehaviour
{
	public string questName;

    protected void updateQuest() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests.updateQuest(questName);
    }
}