using UnityEngine;
using System.Collections;

public class QuestComponentTemplate : MonoBehaviour
{
    public bool questRequired;      // this character only is around when a quest is active
    public string questName;

    protected virtual void Start() {
        if (questRequired) {
            this.gameObject.SetActive(false);
        }
    }

    protected void updateQuest() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMaster>().PlayerQuests.updateQuest(questName);
    }

    public void activateComponent() {
        this.gameObject.SetActive(true);
    }
}