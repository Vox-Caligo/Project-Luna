using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestTitle : MonoBehaviour {
    private string questName;
    private string questDescription;
    private Text descriptionTitle;
    private Text descriptionArea;

    public void setupQuestTitleProperties(string questName, string questDescription, Text descriptionTitle, Text descriptionArea) {
        this.questName = questName;
        this.questDescription = questDescription;
        this.descriptionTitle = descriptionTitle;
        this.descriptionArea = descriptionArea;
        
        // add a click event that changes the quest description to the new one
    }

    // displays the text description in the quest log menu
    public void displayDescription() {
        descriptionTitle.text = questName;
        descriptionArea.text = questDescription;
    }

    public string QuestDescription {
        get { return questDescription; }
    }
}
