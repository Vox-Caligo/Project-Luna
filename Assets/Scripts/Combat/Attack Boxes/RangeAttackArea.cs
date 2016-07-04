using UnityEngine;
using System.Collections;

public class RangeAttackArea : AttackArea
{
	// range area
	protected Vector2 currentLoc;
	protected Vector2 endLoc;
	protected float attackDistance;

	// aoe area
	private float attackAOE;

	// creates the attack area for melee
	public RangeAttackArea(GameObject character, WeaponStats weapon, int currentDirection) : base(character, weapon, currentDirection)
	{	
		collisionDetector.name = "Range Attack";
		this.attackDistance = attackDistance;
		this.attackAOE = attackAOE;

		currentLoc = character.transform.position;

		// endLoc takes the current direction and sends the projectile that way
		//endLoc = 
	}

	protected override void Update ()
	{
		/*
		if(currentLoc < endLoc) {
			// head towards goal
			//this.gameObject.transform.
			currentLoc = this.gameObject.transform.position;
		} else if(explosion) {
			explode();
		} else {
			Destroy(collisionDetector);
		}
		*/
	}
}

