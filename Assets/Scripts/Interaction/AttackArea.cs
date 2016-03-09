using UnityEngine;
using System.Collections;

public class AttackArea : CollisionArea
{
	private bool isHorizontal = false;
	private float attackRange = 2;
	private float attackWidth = 1;

	public AttackArea(GameObject character, string characterName, float attackWidth, float attackRange) : base(character)
	{	
		collisionDetector.name = characterName + " Attack";
		this.attackWidth = attackWidth;
		this.attackRange = attackRange;
	}

	public override void rearrangeCollisionArea(int currentDirection) {
		if(currentDirection % 2 == 0 && !isHorizontal) {
			isHorizontal = true;
			collisionDetector.transform.Rotate(new Vector3(0,0,90));
		} else if (currentDirection % 2 == 1 && isHorizontal) {
			isHorizontal = false;
			collisionDetector.transform.Rotate(new Vector3(0,0,-90));
		}

		if(currentDirection == 0 || currentDirection == 1) { 		
			collisionDetectionBox.offset = new Vector2(0, attackRange); 
		} else {	
			collisionDetectionBox.offset = new Vector2(0, -attackRange);
		} 
	}

	public void resizeHitbox(bool reset, int currentDirection = 0) {
		if (reset) {
			collisionDetectionBox.isTrigger = true;

			if (currentDirection == 0 || currentDirection == 2) {
				collisionDetectionBox.size = new Vector2 (boxHeight, .06f);
			} else {
				collisionDetectionBox.size = new Vector2(.06f, boxWidth);
			} 

		} else {
			collisionDetectionBox.isTrigger = false;
			collisionDetectionBox.size = new Vector2(attackWidth, attackRange);
		}
	}

	public int AttackRange {
		set{attackRange = value;}
	}

	public int AttackWidth {
		set{attackWidth = value;}
	}
}

