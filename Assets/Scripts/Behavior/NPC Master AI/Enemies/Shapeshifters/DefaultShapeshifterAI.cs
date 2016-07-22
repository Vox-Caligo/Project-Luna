using UnityEngine;
using System.Collections;

/**
 * The AI of a minion npc
 */
public class DefaultShapeshifterAI : DefaultAI
{
	protected bool hostile = false;
    protected bool shifted = false;

	// create a minion's combat and movement
	protected override void Start() {
		characterName = "Shapeshifter";
		npcCombat = new MinionCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
	}

	// run through it's thought process
	protected override void processDecisions ()
	{
		// will eventually go through and actually think things through
		npcMovement.TargetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (!shifted) {
            // pursue the player until close enough, then decide to attack
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("test1") as Sprite;

        } else {
            // if far away head towards the player, if close then attack
            if (Vector2.Distance(currentPosition, npcMovement.TargetPoint) > 1.5f/*this.gameObject.GetComponent<BoxCollider2D>().bounds.*/) {

                // determine when the character changes skin (change movement/combat to new character)

                npcMovement.CurrentAction = "pursue";
                npcCombat.CurrentAction = "";
            } else {
                npcMovement.CurrentAction = "nearby-player";
                npcCombat.CurrentAction = "attack";
            }
        }

		base.processDecisions();
	}

	// how to respond to player karma
	protected override void karmaReactions() {
		// have it do things depending on players current karma
	}

	// how to respond to collisions
	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		//npcCombat.respondToCollision(col);
	}
}

