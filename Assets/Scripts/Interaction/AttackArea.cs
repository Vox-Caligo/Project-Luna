using UnityEngine;
using System.Collections;

/**
 * The area that the players attack takes up,
 * thus causing anything inside to take damage
 */
public class AttackArea : CollisionArea
{
	protected int currentDirection;

	// weapon area values
	protected float attackRange = 2;
	protected float attackWidth = 1;

	// sounds
	protected SoundInterpreter sounds;

	// creates the attack area for melee
	public AttackArea(GameObject character, WeaponStats weapon, int currentDirection) : base(character)
	{	
		sounds = new SoundInterpreter(character); // sets character sounds
		sounds.playSound(weapon.Sounds[Random.Range(0, weapon.Sounds.Length)], true);

		this.currentDirection = currentDirection;
		this.attackWidth = weapon.Width;
		this.attackRange = weapon.Length;
		collisionDetectionBox.size = new Vector2 (attackWidth, attackRange);
	}

	protected override void Update () {	}
}