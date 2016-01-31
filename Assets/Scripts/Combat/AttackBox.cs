using UnityEngine;
using System.Collections;

public class AttackBox : MonoBehaviour
{
	private float attackRange = 2;
	private float attackWidth = 1;
	private bool longRange = false;
	private bool isHorz = false;
	
	public AttackBox() {
		attackArea = new GameObject();
		attackArea.transform.parent = gameObject.transform;
		attackArea.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
	}

	// used for generating the appropriate attack hit box (size, direction, height, width)
	public void changeAttackPosition(int currDirection, float atkWidth, float atkLength) {
		
	}
}

