using UnityEngine;
using System.Collections;

public class AttackArea : CollisionArea
{
	public AttackArea(GameObject collisionDetector, string characterName) : base(collisionDetector)
	{	
		collisionDetector.name = characterName + " Attack";
	}

	protected void rearrangeAttackHitbox(int currentDirection) {
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

	protected void resizeHitbox(bool reset, int currentDirection = 0, Vector2 newWeaponHitboxSize = new Vector2()) {
		if (reset) {
			if (currentDirection == 0 || currentDirection == 2) {
				collisionDetectionBox.size = new Vector2 (characterHeight, .06f);
			} else {
				collisionDetectionBox.size = new Vector2(.06f, characterWidth);
			} 

		} else {
			collisionDetectionBox.size = newWeaponHitboxSize;
		}
	}
}

