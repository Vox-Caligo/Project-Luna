using UnityEngine;
using System.Collections;

public class QuestComponentTemplate : MonoBehaviour
{
	protected string questName;
	protected string questDescription;
	protected bool questComplete;

	public QuestComponentTemplate(string questName) {

	}

	public virtual void updateQuest ()
	{

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