﻿using UnityEngine;
using System.Collections;

/**
 * The area that the players attack takes up,
 * thus causing anything inside to take damage
 */
public class AttackArea : CollisionArea
{
	// weapon area values
    protected bool damageDealt = false;

	// sounds
	protected SoundInterpreter sounds;

	// creates the attack area for melee
	public AttackArea(GameObject character, WeaponStats weapon, int currentDirection) : base(character)
	{	
		sounds = new SoundInterpreter(character); // sets character sounds
		sounds.playSound(weapon.Sounds[Random.Range(0, weapon.Sounds.Length)], true);
        
        collisionDetectionBox.size = new Vector2 (weapon.Width, weapon.Length);
        collisionDetectionBox.isTrigger = false;

        if (currentDirection % 2 == 0) {
            collisionDetector.transform.Rotate(new Vector3(0, 0, 90));
        }

        if (currentDirection == 0 || currentDirection == 1) {
            collisionDetectionBox.offset = new Vector2(0, weapon.Length);
        } else {
            collisionDetectionBox.offset = new Vector2(0, -weapon.Length);
        }

        if (weapon.Type == "Melee") {
            collisionDetector.name = "Melee Attack";
            collisionDetector.AddComponent<MeleeAttackArea>().weapon = weapon;
        } else {
            collisionDetector.name = "Range Attack";
            RangeAttackArea rangedAttack = collisionDetector.AddComponent<RangeAttackArea>();
            rangedAttack.weapon = weapon;
            rangedAttack.parent = collisionDetector.transform.parent.name;
            collisionDetector.transform.parent = null;

            // change the 5 to be the weapon range
            if (currentDirection == 0) {
                rangedAttack.endLoc = collisionDetector.transform.position + new Vector3(-weapon.ShotDistance, 0);
            } else if(currentDirection == 1) {
                rangedAttack.endLoc = collisionDetector.transform.position + new Vector3(0, weapon.ShotDistance);
            } else if (currentDirection == 2) {
                rangedAttack.endLoc = collisionDetector.transform.position + new Vector3(weapon.ShotDistance, 0);
            } else {
                rangedAttack.endLoc = collisionDetector.transform.position + new Vector3(0, -weapon.ShotDistance);
            }
        }
    }
}