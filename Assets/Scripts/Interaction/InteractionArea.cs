using UnityEngine;
using System.Collections;

public class InteractionArea : CollisionArea
{
		// Use this for initialization
	public InteractionArea(GameObject player) : base(player)
	{
		collisionDetector.name = "Player Interaction Box";
	}
}

