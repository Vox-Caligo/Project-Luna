using UnityEngine;
using System.Collections;

public class MinionCombatController : DefaultCombatController
{
	public MinionCombatController(string characterName, GameObject character) : base(characterName, character) { }

	public override void runScript() {
		npcCombat.attacking(3);
	}
}

