using UnityEngine;
using System.Collections;

public class CharacterProperties : MonoBehaviour
{
	// current player statuses
	private int health;
	private int mana;
	private bool redoHUD = false;

	/*
	// Use this for initialization
	void Start ()
	{
		health = DialogueLua.GetActorField("Player", "Current Health").AsInt;
		mana = DialogueLua.GetActorField("Player", "Current Mana").AsInt;
	}
	
	void FixedUpdate() {
		if(health != DialogueLua.GetActorField("Player", "Current Health").AsInt || mana != DialogueLua.GetActorField("Player", "Current Mana").AsInt) {
			health = DialogueLua.GetActorField("Player", "Current Health").AsInt;
			mana = DialogueLua.GetActorField("Player", "Current Mana").AsInt;
			redoHUD = true;
		}
	}
	*/
}