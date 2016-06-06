using UnityEngine;
using System.Collections;

public class LocationQuestComponent : QuestComponentTemplate
{
    public int locationNumber;

    // Use this for initialization
    void Start() {
    }
	
	void Update () 
	{
		// if location is not discovered

		// if location is discovered
	}

    public override void updateQuest() {
        locateQuestLog();
        questLog.updateLocationQuest(questName, locationNumber);
    }
}