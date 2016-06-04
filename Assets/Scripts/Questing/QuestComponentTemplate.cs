using UnityEngine;
using System.Collections;

public class QuestComponentTemplate : MonoBehaviour
{
	public string questName;
	protected string questDescription;
	protected bool questComplete;

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