using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestLogUI : MonoBehaviour {
    public ArrayList activeQuestNames = new ArrayList();
    public bool invisible = true;

    // Use this for initialization
    void Start() {
        activeQuestNames.Add("Test Quest 1");
        activeQuestNames.Add("Test Quest 2");
        activeQuestNames.Add("Test Quest 3");
        activeQuestNames.Add("Test Quest 4");
        activeQuestNames.Add("Test Quest 5");
        addQuest("Moo", "Cow");
    }

    public void addQuest(string questName, string questDescription) {
        //GameObject newQuestTitle = Instantiate(Resources.Load("UI/Quest Title", typeof(GameObject))) as GameObject;
        GameObject newQuestTitle = GameObject.Find("Quest Title");

        if (newQuestTitle != null) {
            print("Made it");
        }
        else {
            print("Not made it");
        }

        print("This is here: " + newQuestTitle.name);
        newQuestTitle.GetComponent<Text>().text = "Moo Cow Goes Moo!";
        //newQuestTitle.transform.parent = GameObject.Find("Quest Log").transform;
        print("HERE");
    }

    // checks if the display is visible
    public bool Invisible {
        get { return invisible; }
        set { invisible = value; }
    }
}
