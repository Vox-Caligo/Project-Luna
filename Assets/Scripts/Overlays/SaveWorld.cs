using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class SaveWorld : MonoBehaviour {
	public void continueGame() {
		pullStats();
	}
	
	// pulls the stats from the player's computer storage
	public void pullStats() {
		DialogueLua.SetActorField("Player", "OnLevel", PlayerPrefs.GetInt("OnLevel"));
		DialogueLua.SetActorField("Player", "Gold", PlayerPrefs.GetInt("Gold"));
		DialogueLua.SetActorField("Player", "Level", PlayerPrefs.GetInt("Level"));
		DialogueLua.SetActorField("Player", "Experience", PlayerPrefs.GetInt("Experience"));
		DialogueLua.SetActorField("Player", "ExpToLevel", PlayerPrefs.GetInt("ExpToLevel"));
		DialogueLua.SetActorField("Player", "Level_Points", PlayerPrefs.GetInt("Level_Points"));
		DialogueLua.SetActorField("Player", "Max_Health", PlayerPrefs.GetInt("Max_Health"));
		DialogueLua.SetActorField("Player", "Current_Health", PlayerPrefs.GetInt("Current_Health"));
		DialogueLua.SetActorField("Player", "Max_Mana", PlayerPrefs.GetInt("Max_Mana"));
		DialogueLua.SetActorField("Player", "Current_Mana", PlayerPrefs.GetInt("Current_Mana"));
		DialogueLua.SetActorField("Player", "Strength", PlayerPrefs.GetInt("Strength"));
		DialogueLua.SetActorField("Player", "Wisdom", PlayerPrefs.GetInt("Wisdom"));
		DialogueLua.SetActorField("Player", "Agility", PlayerPrefs.GetInt("Agility"));
		DialogueLua.SetActorField("Player", "Slashing_Damage", PlayerPrefs.GetInt("Slashing_Damage"));
		DialogueLua.SetActorField("Player", "Slashing_Defence", PlayerPrefs.GetInt("Slashing_Defence"));
		DialogueLua.SetActorField("Player", "Crushing_Damage", PlayerPrefs.GetInt("Crushing_Damage"));
		DialogueLua.SetActorField("Player", "Crushing_Defence", PlayerPrefs.GetInt("Crushing_Defence"));
		DialogueLua.SetActorField("Player", "Piercing_Damage", PlayerPrefs.GetInt("Piercing_Damage"));
		DialogueLua.SetActorField("Player", "Piercing_Defence", PlayerPrefs.GetInt("Piercing_Defence"));
		DialogueLua.SetActorField("Player", "Fire_Defence", PlayerPrefs.GetInt("Fire_Defence"));
		DialogueLua.SetActorField("Player", "Water_Damage", PlayerPrefs.GetInt("Water_Damage"));
		DialogueLua.SetActorField("Player", "Water_Defence", PlayerPrefs.GetInt("Water_Defence"));
		DialogueLua.SetActorField("Player", "Lightning_Damage", PlayerPrefs.GetInt("Lightning_Damage"));
		DialogueLua.SetActorField("Player", "Lightning_Defence", PlayerPrefs.GetInt("Lightning_Defence"));
		DialogueLua.SetActorField("Player", "Holy_Damage", PlayerPrefs.GetInt("Holy_Damage"));
		DialogueLua.SetActorField("Player", "Holy_Defence", PlayerPrefs.GetInt("Holy_Defence"));
		DialogueLua.SetActorField("Player", "Dark_Damage", PlayerPrefs.GetInt("Dark_Damage"));
		DialogueLua.SetActorField("Player", "Dark_Defence", PlayerPrefs.GetInt("Dark_Defence"));
		DialogueLua.SetActorField("Player", "Critical", PlayerPrefs.GetInt("Critical"));
		DialogueLua.SetActorField("Player", "Visibility", PlayerPrefs.GetInt("Visibility"));
		DialogueLua.SetActorField("Player", "Charisma", PlayerPrefs.GetInt("Charisma"));
		DialogueLua.SetActorField("Player", "Silence", PlayerPrefs.GetInt("Silence"));
	}
	
	// stores the stats to the player's computer storage
	public void storeStats() {
		PlayerPrefs.SetInt("OnLevel", Application.loadedLevel);
		PlayerPrefs.SetInt("Gold", DialogueLua.GetActorField("Player", "Gold").AsInt);
		PlayerPrefs.SetInt("Level", DialogueLua.GetActorField("Player", "Level").AsInt);
		PlayerPrefs.SetInt("Experience", DialogueLua.GetActorField("Player", "Experience").AsInt);
		PlayerPrefs.SetInt("ExpToLevel", DialogueLua.GetActorField("Player", "ExpToLevel").AsInt);
		PlayerPrefs.SetInt("Level_Points", DialogueLua.GetActorField("Player", "Level_Points").AsInt);
		PlayerPrefs.SetInt("Max_Health", DialogueLua.GetActorField("Player", "Max_Health").AsInt);
		PlayerPrefs.SetInt("Current_Health", DialogueLua.GetActorField("Player", "Current_Health").AsInt);
		PlayerPrefs.SetInt("Max_Mana", DialogueLua.GetActorField("Player", "Max_Mana").AsInt);
		PlayerPrefs.SetInt("Current_Mana", DialogueLua.GetActorField("Player", "Current_Mana").AsInt);
		PlayerPrefs.SetInt("Strength", DialogueLua.GetActorField("Player", "Strength").AsInt);
		PlayerPrefs.SetInt("Wisdom", DialogueLua.GetActorField("Player", "Wisdom").AsInt);
		PlayerPrefs.SetInt("Agility", DialogueLua.GetActorField("Player", "Agility").AsInt);
		PlayerPrefs.SetInt("Slashing_Damage", DialogueLua.GetActorField("Player", "Slashing_Damage").AsInt);
		PlayerPrefs.SetInt("Slashing_Defence", DialogueLua.GetActorField("Player", "Slashing_Defence").AsInt);
		PlayerPrefs.SetInt("Crushing_Damage", DialogueLua.GetActorField("Player", "Crushing_Damage").AsInt);
		PlayerPrefs.SetInt("Crushing_Defence", DialogueLua.GetActorField("Player", "Crushing_Defence").AsInt);
		PlayerPrefs.SetInt("Piercing_Damage", DialogueLua.GetActorField("Player", "Piercing_Damage").AsInt);
		PlayerPrefs.SetInt("Piercing_Defence", DialogueLua.GetActorField("Player", "Piercing_Defence").AsInt);
		PlayerPrefs.SetInt("Fire_Damage", DialogueLua.GetActorField("Player", "Fire_Damage").AsInt);
		PlayerPrefs.SetInt("Fire_Defence", DialogueLua.GetActorField("Player", "Fire_Defence").AsInt);
		PlayerPrefs.SetInt("Water_Damage", DialogueLua.GetActorField("Player", "Water_Damage").AsInt);
		PlayerPrefs.SetInt("Water_Defence", DialogueLua.GetActorField("Player", "Water_Defence").AsInt);
		PlayerPrefs.SetInt("Lightning_Damage", DialogueLua.GetActorField("Player", "Lightning_Damage").AsInt);
		PlayerPrefs.SetInt("Lightning_Defence", DialogueLua.GetActorField("Player", "Lightning_Defence").AsInt);
		PlayerPrefs.SetInt("Holy_Damage", DialogueLua.GetActorField("Player", "Holy_Damage").AsInt);
		PlayerPrefs.SetInt("Holy_Defence", DialogueLua.GetActorField("Player", "Holy_Defence").AsInt);
		PlayerPrefs.SetInt("Dark_Damage", DialogueLua.GetActorField("Player", "Dark_Damage").AsInt);
		PlayerPrefs.SetInt("Dark_Defence", DialogueLua.GetActorField("Player", "Dark_Defence").AsInt);
		PlayerPrefs.SetInt("Visibility", DialogueLua.GetActorField("Player", "Visibility").AsInt);
		PlayerPrefs.SetInt("Critical", DialogueLua.GetActorField("Player", "Critical").AsInt);
		PlayerPrefs.SetInt("Charisma", DialogueLua.GetActorField("Player", "Charisma").AsInt);
		PlayerPrefs.SetInt("Silence", DialogueLua.GetActorField("Player", "Silence").AsInt);
		
		PlayerPrefs.SetFloat("xLocation", GameObject.FindGameObjectWithTag("Player").transform.position.x);
		PlayerPrefs.SetFloat("yLocation", GameObject.FindGameObjectWithTag("Player").transform.position.y);
	}
	
	public void storeInventory(string items) {
		PlayerPrefs.SetString("Inventory", items);
	}
	
	public string pullInventory() {
		return PlayerPrefs.GetString("Inventory");
	}
	
	// sets stats for a new game
	public void newGame() {
		PlayerPrefs.SetInt("OnLevel", 1);
		PlayerPrefs.SetInt("Gold",200); 
		PlayerPrefs.SetInt("Level",1); 
		PlayerPrefs.SetInt("Experience",0); 
		PlayerPrefs.SetInt("ExpToLevel",3); 
		PlayerPrefs.SetInt("Level_Points",0); 
		PlayerPrefs.SetInt("Max_Health",18); 
		PlayerPrefs.SetInt("Current_Health",18); 
		PlayerPrefs.SetInt("Max_Mana",5); 
		PlayerPrefs.SetInt("Current_Mana",5); 
		PlayerPrefs.SetInt("Strength",0); 
		PlayerPrefs.SetInt("Wisdom",0); 
		PlayerPrefs.SetInt("Agility",0); 
		PlayerPrefs.SetInt("Slashing_Damage",0); 
		PlayerPrefs.SetInt("Slashing_Defence",0); 
		PlayerPrefs.SetInt("Crushing_Damage",0); 
		PlayerPrefs.SetInt("Crushing_Defence",0); 
		PlayerPrefs.SetInt("Piercing_Damage",0); 
		PlayerPrefs.SetInt("Piercing_Defence",0); 
		PlayerPrefs.SetInt("Fire_Damage",0); 
		PlayerPrefs.SetInt("Fire_Defence",0); 
		PlayerPrefs.SetInt("Water_Damage",0); 
		PlayerPrefs.SetInt("Water_Defence",0); 
		PlayerPrefs.SetInt("Lightning_Damage",0); 
		PlayerPrefs.SetInt("Lightning_Defence",0); 
		PlayerPrefs.SetInt("Holy_Damage",0); 
		PlayerPrefs.SetInt("Holy_Defence",0); 
		PlayerPrefs.SetInt("Dark_Damage",0); 
		PlayerPrefs.SetInt("Dark_Defence",0); 
		PlayerPrefs.SetInt("Visibility",0); 
		PlayerPrefs.SetInt("Critical",0); 
		PlayerPrefs.SetInt("Charisma",0); 
		PlayerPrefs.SetInt("Silence",0);
		
		PlayerPrefs.SetFloat("xLocation",108.5f); 
		PlayerPrefs.SetFloat("yLocation",-1.2f);
		
		PlayerPrefs.SetString("Inventory", "");
	}
}
