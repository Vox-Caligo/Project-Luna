using UnityEngine;
using System.Collections;

public class MinionAI : DefaultAI
{
	protected bool hostile = false;

	protected override void Start() {
		characterName = "Minion";
		npcCombat = new MinionCombatController(characterName, this.gameObject);
		npcMovement = new MinionMovementController(characterName, this.gameObject);
	}
	
	protected override void processDecisions ()
	{
		// will eventually go through and actually think things through
		npcMovement.TargetPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

		// pursue the player until close enough, then decide to attack

		if(Vector2.Distance(currentPosition, npcMovement.TargetPoint) > 1.5f/*this.gameObject.GetComponent<BoxCollider2D>().bounds.*/) {
			npcMovement.CurrentAction = "pursue";
			npcCombat.CurrentAction = "";
		} else {
			npcMovement.CurrentAction = "nearby-player";
			npcCombat.CurrentAction = "attack";
		}

		npcMovement.CurrentAction = "halt";
		npcCombat.CurrentAction = "";
		npcMovement.runScript();
		npcCombat.runScript(npcMovement.CurrentDirection);
		base.processDecisions();
	}

	protected override void OnCollisionEnter2D (Collision2D col) {
		base.OnCollisionEnter2D(col);
		npcMovement.respondToCollision(col);
		npcCombat.respondToCollision(col);
	}
}

