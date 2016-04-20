using UnityEngine;
using System.Collections;

/**
 * The area that the players attack takes up,
 * thus causing anything inside to take damage
 */
public class AttackArea : CollisionArea
{
	// if the player is walking left/right
	private bool isHorizontal = false;

	// weapon area values
	private float attackRange = 2;
	private float attackWidth = 1;

	// creates the attack area
	public AttackArea(GameObject character, string characterName, float attackWidth, float attackRange) : base(character)
	{	
		collisionDetector.name = characterName + " Attack";
		this.attackWidth = attackWidth;
		this.attackRange = attackRange;
	}

	// rearranges the area by the way the player is walking
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

	// turns the attack area on and off depending on what is needed
	public void manipulateAttackArea(bool active, int currentDirection = 0) {
		if (active) {
			collisionDetectionBox.isTrigger = false;
			collisionDetectionBox.size = new Vector2 (attackWidth, attackRange);
			rearrangeCollisionArea (currentDirection);
		} else {
			collisionDetectionBox.isTrigger = true;
			collisionDetectionBox.size = new Vector2 (.01f, .01f);
			collisionDetectionBox.offset = new Vector2(0, 0); 
		}
	}

	// gets the current range of the attack
	public int AttackRange {
		set{attackRange = value;}
	}

	// gets the current width of the attack
	public int AttackWidth {
		set{attackWidth = value;}
	}
}

