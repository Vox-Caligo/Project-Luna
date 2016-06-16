using UnityEngine;
using System.Collections;

public class LocationQuestComponent : QuestComponentTemplate
{
    // Use this for initialization
    protected override void Start() {
        base.Start();
    }
	
    // DETECT IF COLLISION OCCURS
    private void OnTriggerEnter2D(Collider2D player) {
        if(player.gameObject.GetComponent<PlayerMaster>()) {
            this.updateQuest();
            //Destroy(this);
        }
    }
}