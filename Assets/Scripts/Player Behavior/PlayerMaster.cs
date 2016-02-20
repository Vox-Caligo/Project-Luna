using UnityEngine;
using System.Collections;

public class PlayerMaster : MonoBehaviour {
	private PlayerCombat playerCombat;
	private PlayerMovement playerMovement;
	private int currentDirection = 0;

	// know when/where to move
	// know when to attack

	// Use this for initialization
	public virtual void Start() {
		playerCombat = new PlayerCombat("Player", this.gameObject);
		playerMovement = new PlayerMovement(this.gameObject);
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		playerMovement.walk();
		playerCombat.updatePlayerCombat();
	}
}
