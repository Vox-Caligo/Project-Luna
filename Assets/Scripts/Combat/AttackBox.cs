using UnityEngine;
using System.Collections;

public class AttackBox : MonoBehaviour
{
	private GameObject attackArea;
	
	public AttackBox() {
		attackArea = new GameObject();
		attackArea.transform.parent = gameObject.transform;
		attackArea.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
	}

	// used for generating the appropriate attack hit box (size, direction, height, width)
	public void changeAttackPosition(int currDirection, float atkWidth, float atkLength) {
		/*if(!inAttack && ( magic with mana DialogueLua.GetActorField("Player", "Current Mana").AsInt > -1) or non magic)*/
		if (1 == 1) {
			inAttack = true;
			
			if(currDirection == 0) { attackArea.transform.position = new Vector3(gameObject.transform.position.x - 1, gameObject.transform.position.y);
			} else if(currDirection == 1) {	attackArea.transform.position = new Vector3(gameObject.transform.position.x + 1, gameObject.transform.position.y);
			} else if(currDirection == 2) { attackArea.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1);
			} else if(currDirection == 3) {	attackArea.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1);
			}
			
			BoxCollider2D newView = attackArea.AddComponent<BoxCollider2D>();
			
			if(atkWidth < .5f) 
				atkWidth = .5f;
			if(atkLength < .5f) 
				atkLength = .5f;
			
			newView.size = new Vector2(atkWidth, atkLength);
			
			if((currDirection <= 1 && !isHorz) || (currDirection > 1 && isHorz)) {		// left and  right
				isHorz = !isHorz;
				attackArea.transform.Rotate(new Vector3(0,0,90));
			}
			
			newView.isTrigger = true;
			newView.name = "Player Attack";
		}
	}
}

