using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestTitle : MonoBehaviour {
    private string questDescription;
    private Text descriptionArea;

    public void setupQuestTitleProperties(string questDescription, Text descriptionArea) {
        this.questDescription = questDescription;
        this.descriptionArea = descriptionArea;

        descriptionArea.text = questDescription;
        // add a click event that changes the quest description to the new one
    }

    // displays the text description in the quest log menu
    public void displayDescription() {
        descriptionArea.text = questDescription;
    }

    public string QuestDescription {
        get { return questDescription; }
    }
}
