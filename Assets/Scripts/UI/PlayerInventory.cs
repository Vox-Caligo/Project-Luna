using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// holds some standard information about the item and where it's located
class OldInventory {
	string name;
	string type;
	
	int slotNum;
	
	public OldInventory(string n, int s) {
		name = n;
		slotNum = s;
	}
	
	public string itemName (){return name;}
	public void setName(string newName) {name = newName;}
	public int slotNumber() {return slotNum;}
}

// used to display information about the items in the inventory
public class PlayerInventory : MonoBehaviour {
	public ArrayList inv = new ArrayList();
	
	private bool[] filledSpot = new bool[10];
	public Button[] inventorySpots = new Button[10];
	public string[] spotsFilledWith = new string[10];
	public Image highlightedItem;
	bool visible = false;
	
	// used for equipped items
	public Button[] wornItems = new Button[6];
	
	public Button useButton;
	public Button discardButton;
	
	public Text itemTitle;
	public Text itemDescription;
	
	private string chosenItem;
	private int chosenSlot;
	
	UnityEngine.UI.Image invImg;
	
	public GameObject gold;
	public string currentMedallion = "";
	
	public bool isShown = false;
	
	//public ItemEffects myItemEffects;	// creates item effects or puts on armor/weapons
	/*
	void Start() {
		Lua.RegisterFunction("checkInventory", this, typeof(PlayerInventory).GetMethod("checkInventory"));
		
		useButton.onClick.AddListener(()=>{useItem();});
		discardButton.onClick.AddListener(()=>{removeFromInventory(false);});
		
		for(int i = 0; i < filledSpot.Length - 1; i++) {
			filledSpot[i] = false;
			int captured = i;
			inventorySpots[i].onClick.AddListener (()=>{displayItem(captured);});
		}
		
		for(int i = 0; i < wornItems.Length; i++) {
			int captured = i;
			wornItems[i].onClick.AddListener (()=>{removeWornItem(captured);});
		}
		
//		myItemEffects = new ItemEffects();
	}
	
	// displays the item and gives the description and item name in the UI
	public void displayItem(int slotNum) {
		int checkItem = 0;
		foreach (Inventory i in inv) {
			if(i.slotNumber() == slotNum) {
				break;
			}
			checkItem++;
		}
		
		Inventory myInv = inv[checkItem] as Inventory;
		fillInformation(false, slotNum, myInv.itemName(), DialogueLua.GetItemField(myInv.itemName(), "Description").AsString);
	}
	
	// adds the item to the player's inventory
	public void addToInventory(string item) {	
		for(int i = 0; i < filledSpot.Length - 1; i++) {
			Debug.Log("Space " + i + " contains " + spotsFilledWith[i]);
			Debug.Log("Space " + i + " isn't empty? " + filledSpot[i]);
		}
		Debug.Log("Check 0: " + item);
		for(int i = 0; i < filledSpot.Length - 1; i++) {
			if(spotsFilledWith[i] == null || spotsFilledWith[i] == "")
				filledSpot[i] = false;
			else
				filledSpot[i] = true;
			
			if(filledSpot[i] == false) {
				Debug.Log("Space " + i + " is empty");
				Debug.Log("Check 1: " + item);
				filledSpot[i] = true;
				spotsFilledWith[i] = item;
				
				foreach (Inventory j in inv) {
					if(j.slotNumber() == i) {
						j.setName(item);
						break;
					}
				}
				
				invImg = inventorySpots[i].GetComponent<UnityEngine.UI.Image>();
				invImg.overrideSprite = Resources.Load<Sprite>("Items/" + item);
				inv.Add(new Inventory(item, i));
				break;
			}
		}
	}
	
	// checks if the item is in the player's inventory
	public bool checkInventory(string item) {
		for(int i = 0; i < spotsFilledWith.Length - 1; i++) {
			if(spotsFilledWith[i] == item)
				return true;
		}
		return false;
	}
	
	// used for saving current items
	public string currentInventoryItems() {
		string myItems = "";
	
		for(int i = 0; i < spotsFilledWith.Length - 1; i++) {
			if(spotsFilledWith[i].Length > 0) {
				myItems = myItems + spotsFilledWith[i] + "|";
			}
		}
		return myItems;
	}
	
	// removes a worn item and puts back into inventory
	public void removeWornItem(int i) {
//		myItemEffects.removeWornItem(i);
	}
	
	// uses the item and removes it from the player's inventory
	public void useItem() {
		// stores values used mainly for switching on and off equipped weapons	
		string currItem = chosenItem;
		Sprite currSlot = (inventorySpots[chosenSlot].GetComponent<UnityEngine.UI.Image>()).overrideSprite;
		
		removeFromInventory(true);		
		
//		myItemEffects.setUp(inventorySpots, this);
//		myItemEffects.applyEffect(currItem, currSlot);
	}
	
	// removes the item from the players inventory
	public void removeFromInventory(bool beingUsed) {
		int removedItem = 0;
		string itemName = "";
		
		foreach (Inventory i in inv) {
			if(i.itemName() == chosenItem) {
				itemName = i.itemName();
				break;
			}
			removedItem++;
		}
		
		if(!DialogueLua.GetItemField(itemName, "Quest Item").AsBool || beingUsed) {
			inv.Remove(chosenItem);
			Inventory holder = inv[removedItem] as Inventory;
			holder.setName("");
			fillInformation(true, chosenSlot);
			inventorySpots[chosenSlot].GetComponent<UnityEngine.UI.Image>().overrideSprite = null;
			filledSpot[chosenSlot] = false;
			spotsFilledWith[chosenSlot] = "";
		} else {
			Debug.Log ("Is quest item");
		}
	}
	
	// removes the item from inventory when sold (try and combine with the previous method)
	public void sellItem(string item) {
		for(int i = 0; i < spotsFilledWith.Length - 1; i++) {
			if(spotsFilledWith[i] == item) {
				spotsFilledWith[i] = "";
				inventorySpots[i].GetComponent<UnityEngine.UI.Image>().overrideSprite = null;
				filledSpot[i] = false;
				break;
			}
		}
		inv.Remove(item);
	}
	
	// fills out the information and highlights the appropriate area
	public void fillInformation(bool empty, int slotNum, string itemNm="", string itemDesc="") {
		if(empty){
			highlightedItem.GetComponent<UnityEngine.UI.Image>().overrideSprite = null;
			spotsFilledWith[slotNum] = "";
		} else {
			Sprite tester = (inventorySpots[slotNum].GetComponent<UnityEngine.UI.Image>()).overrideSprite;
			highlightedItem.GetComponent<UnityEngine.UI.Image>().overrideSprite = tester;
			
			chosenSlot = slotNum;
			spotsFilledWith[slotNum] = itemNm;
		}
		
		chosenItem = itemNm;
		itemTitle.text = "Item: " + itemNm;
		itemDescription.text = "Item Description: " + itemDesc;
	}
	
	// can turn the UI on and off
	void Update() {
		if( Input.GetKeyDown( KeyCode.E ))	{
			gold.GetComponent<Text>().text = DialogueLua.GetActorField("Player", "Gold").AsString;
		}
	}
	
	// changes the interactability and visibility
	public void changeVisibility(bool makeViewable) { 
		if(makeViewable) {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 1;
			gameObject.GetComponentInChildren<CanvasGroup>().interactable = true;
			gameObject.GetComponentInChildren<CanvasGroup>().blocksRaycasts = true;
			isShown = true;
		} else {
			gameObject.GetComponentInChildren<CanvasGroup>().alpha = 0;
			gameObject.GetComponentInChildren<CanvasGroup>().interactable = false;
			gameObject.GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;
			isShown = false;
		}
	}
	*/
}