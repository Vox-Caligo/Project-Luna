using UnityEngine;
using System.Collections;

/**
 * A small area in front of the player that can
 * be used to interact with objects/characters.
 */
public class InteractionArea : CollisionArea
{
		// Use this for initialization
	public InteractionArea(GameObject player) : base(player)
	{
		collisionDetector.name = "Player Interaction Box";
	}
}

