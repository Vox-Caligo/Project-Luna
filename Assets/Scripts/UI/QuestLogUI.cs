using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestLogUI : MonoBehaviour {
    private Dictionary<string, GameObject> questTitles = new Dictionary<string, GameObject>();

    public bool invisible = true;

    public void addQuest(string questName, string questDescription) {
        GameObject newQuestTitle = Instantiate(Resources.Load("UI/Quest Title", typeof(GameObject))) as GameObject;

        Text newTitleTextStyle = newQuestTitle.GetComponent<Text>();
        newTitleTextStyle.text = questName;
        newTitleTextStyle.color = Color.black;
        newTitleTextStyle.fontStyle = FontStyle.Normal;

        // add to quest title panel
        Transform questTitlePanel = this.gameObject.transform.FindChild("Quest Titles");
        newQuestTitle.transform.parent = questTitlePanel;
        newQuestTitle.transform.localPosition = new Vector3(0, -25 + (questTitles.Count * -25));

        // new quest is clickable 
        newQuestTitle.GetComponent<QuestTitle>().setupQuestTitleProperties(questName, questDescription, 
                                                                            transform.Find("Inner Title").gameObject.GetComponent<Text>(), 
                                                                            transform.Find("Description").gameObject.GetComponent<Text>());
        questTitles.Add(questName, newQuestTitle);
    }

    public void expireQuest(string questName) {
        if (questTitles.ContainsKey(questName)) {
            Text newTitleTextStyle = questTitles[questName].GetComponent<Text>();
            newTitleTextStyle.text = questName;
            newTitleTextStyle.color = Color.white;
            newTitleTextStyle.fontStyle = FontStyle.Italic;
        }
    }

    // checks if the display is visible
    public bool Invisible {
        get { return invisible; }
        set { invisible = value; }
    }
}
