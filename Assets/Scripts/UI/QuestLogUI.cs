using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestLogUI : MonoBehaviour {
    public ArrayList activeQuestNames = new ArrayList();
    public bool invisible = true;

    public void addQuest(string questName, string questDescription) {
        GameObject newQuestTitle = Instantiate(Resources.Load("UI/Quest Title", typeof(GameObject))) as GameObject;
        newQuestTitle.GetComponent<Text>().text = questName;
        newQuestTitle.transform.parent = this.gameObject.transform;
        newQuestTitle.GetComponent<QuestTitle>().setupQuestTitleProperties(questDescription, transform.Find("Quest Description").gameObject.GetComponent<Text>());
    }

    // checks if the display is visible
    public bool Invisible {
        get { return invisible; }
        set { invisible = value; }
    }
}
