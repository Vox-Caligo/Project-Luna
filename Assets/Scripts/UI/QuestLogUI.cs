using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestLogUI : MonoBehaviour {
    private Dictionary<string, GameObject> questInformationButtons = new Dictionary<string, GameObject>();

    public bool invisible = true;

    public void updateUI(Quest updatedQuest) {
        if(!questInformationButtons.ContainsKey(updatedQuest.QuestName)) {
            addQuest(updatedQuest);
        }

        if (updatedQuest.QuestCompleted) {
            expireQuest(updatedQuest.QuestName);
        }
    }

    private void addQuest(Quest updatedQuest) {
        GameObject newQuestInformationButton = Instantiate(Resources.Load("UI/Quest Information Button", typeof(GameObject))) as GameObject;

        Text newTitleTextStyle = newQuestInformationButton.GetComponent<Text>();
        newTitleTextStyle.text = updatedQuest.QuestName;
        newTitleTextStyle.color = Color.black;
        newTitleTextStyle.fontStyle = FontStyle.Normal;

        // add to quest title panel
        Transform questTitlePanel = this.gameObject.transform.FindChild("Quest Information Buttons");
        newQuestInformationButton.transform.parent = questTitlePanel;
        newQuestInformationButton.transform.localPosition = new Vector3(0, -25 + (questInformationButtons.Count * -25));

        // new quest is clickable 
        newQuestInformationButton.GetComponent<QuestTitle>().setupQuestTitleProperties(updatedQuest.QuestName, updatedQuest.QuestDescription, 
                                                                            transform.Find("Title").gameObject.GetComponent<Text>(), 
                                                                            transform.Find("Description").gameObject.GetComponent<Text>());
        questInformationButtons.Add(updatedQuest.QuestName, newQuestInformationButton);
    }

    private void expireQuest(string questName) {
        if (questInformationButtons.ContainsKey(questName)) {
            Text newTitleTextStyle = questInformationButtons[questName].GetComponent<Text>();
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
