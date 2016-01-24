using UnityEngine;
using System.Collections;

public class Pursuing : MonoBehaviour
{
	private GameObject character;
	private GameObject targetCharacter;
	
	public Pursuing (GameObject character) {
		this.character = character;
	}
	
	// Use this for initialization
	void Start ()
	{

	}

	public int inPursuit() {
		float targetX = targetCharacter.rigidbody.position.x;
		float targetY = targetCharacter.rigidbody.position.y;
		float currentX = this.character.rigidbody2D.position.x;
		float currentY = this.character.rigidbody2D.position.y;
	
		float xDistance = Vector2.Distance(new Vector2(currentX, 0), new Vector2(targetX, 0));
		float yDistance = Vector2.Distance(new Vector2(0, currentY), new Vector2(0, targetY));
		
		if(xDistance >= yDistance) {
			if(currentX > targetX) {
				// testing more
				return 0;
			} else {
			
				return 0;
			}
		} else {
			if(currentY > targetY) {
				
				return 0;
			} else {
				
				
			}
		}
	}
	
	public GameObject TargetCharacter {
		set {targetCharacter = value;}
	}
}