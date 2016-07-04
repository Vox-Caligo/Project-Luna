using UnityEngine;
using System.Collections;

public class MeleeAttackArea : AttackArea
{
	// creates the attack area for melee
	public MeleeAttackArea(GameObject character, WeaponStats weapon, int currentDirection) : base(character, weapon, currentDirection)
	{	
		collisionDetector.name = "Melee Attack";
	}

	protected override void Update ()
	{
		/*
		if (time < expirationTime && slashing) {
			rotate();	
		} else {
			Destroy(collisionDetector);
		}
		*/
	}
}

