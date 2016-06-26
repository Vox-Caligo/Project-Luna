using UnityEngine;
using System.Collections;

public class LocationQuestComponent : QuestComponentTemplate
{
    // DETECT IF COLLISION OCCURS
    private void OnTriggerEnter2D(Collider2D player) {
        if(player.gameObject.GetComponent<PlayerMaster>()) {
            this.updateQuest();
            //Destroy(this);
        }
    }
}