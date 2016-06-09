using UnityEngine;
using System.Collections;

public class QuestComponentTemplate : MonoBehaviour
{
    protected QuestLog questLog;
	public string questName;
	protected string questDescription;
	protected bool questComplete;

    protected virtual void Start() {
        questLog = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests;
    }

	public void updateQuest () {
        questLog.updateQuest(questName);
    }

	public string QuestName {
		get { return questName; }
	}

	public string QuestDescription {
		get { return questDescription; }
	}

	public bool QuestComplete {
		get { return questComplete; }
	}
}