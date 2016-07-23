using UnityEngine;
using System.Collections;

/**
 * The AI of a simple villager NPC
 */
public class DefaultFollowerAI : DefaultAI
{
	protected bool hostile = false;
    protected GameObject targetCharacter;
    protected bool followAtSpeed = false;

	// set the combat, movement, and check the players karma
	protected override void Start() {
		characterName = "Follower";
		npcCombat = new VillagerCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
		playerKarma = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerMaster>().currentCharacterCombat();
        targetCharacter = GameObject.FindGameObjectWithTag("Player");

    }

	// run through ai
	protected override void processDecisions ()
	{
		base.processDecisions();
	}

	// how to respond to the players current karma level
	protected override void karmaReactions() {
		// flee if negative karma
		npcMovement.TargetPoint = targetCharacter.transform.position;
        npcMovement.TargetDirection = targetCharacter.GetComponent<PlayerMaster>().currentCharacterMovement().CurrentDirection;
        npcMovement.FollowSpeed = 1.5f; // change this to be character speed or players depending on actions

        npcMovement.CurrentAction = "follow";
		npcCombat.CurrentAction = "";
	}

	// how to respond to a collision
	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		//npcCombat.respondToCollision(col);
	}
}

