using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	/*
	public class SequencerCommandShoppingItems : SequencerCommand {
		
		public void Start() {
			// stores if the player is buying/selling, which shop keep it is, and what the item is
			bool buying = GetParameterAsBool(0);
			string shopKeeper = GetParameter(1).Trim(new char[] {'"'});
			string item = GetParameter(2).Trim(new char[] {'"'});
			
			// gets the player and shop keep
//			PlayerScript aegis = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
			GameObject merchant = GameObject.Find(shopKeeper);
						
			if(buying){
				// checks if the player can buy the item and if so, subtracts the cash correctly and adds the item to the player's inventory
				if (DialogueLua.GetActorField("Player", "Gold").AsInt >= DialogueLua.GetItemField(item, "Cost").AsInt){
					DialogueLua.SetActorField("Player", "Gold", DialogueLua.GetActorField("Player", "Gold").AsInt - DialogueLua.GetItemField(item, "Cost").AsInt);
					DialogueLua.SetActorField(shopKeeper, "Gold", DialogueLua.GetActorField(shopKeeper, "Gold").AsInt + DialogueLua.GetItemField(item, "Cost").AsInt);
//					aegis.aegisInventory.addToInventory(item);
				} else {
					DialogueLua.SetActorField("Player", "Enough Money", false);
				}
			} else {
				// checks if selling if the merchant has enough money and if so removes the item from the player's inventory and compensates them
//				if(aegis.aegisInventory.checkInventory(item)) {
					int itemCost = DialogueLua.GetItemField(item, "Cost").AsInt;
	                int paidGold = (int)(itemCost * .33);
	                
//	                aegis.aegisInventory.sellItem(item);
	                
					DialogueLua.SetActorField("Player", "Gold", DialogueLua.GetActorField("Player", "Gold").AsInt + paidGold);
					DialogueLua.SetActorField(shopKeeper, "Gold", DialogueLua.GetActorField(shopKeeper, "Gold").AsInt - paidGold);
				}
			}
				
			Stop();
		}
	}
	*/
}