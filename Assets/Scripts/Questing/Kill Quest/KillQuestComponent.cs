using UnityEngine;
using System.Collections;

public class KillQuestComponent : QuestComponentTemplate
{
	private DefaultAI npcToKill;

	// applied to an npc that can be killed
	void Start ()
	{
		npcToKill = this.gameObject.GetComponent<DefaultAI> ();
	}

	// Update is called once per frame
	void Update ()
	{
		if (npcToKill.characterHealth() == 0) {
			print ("UPDATE WIT DA KILL!");
		}
	}
}

