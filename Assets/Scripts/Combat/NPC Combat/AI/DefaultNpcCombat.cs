using UnityEngine;
using System.Collections;

public class DefaultNpcCombat : Combat
{
	public DefaultNpcCombat(string characterName, GameObject character, string characterWeapon) : base(characterName, character, characterWeapon) {	}

	public void updateNpcCombat(int currentDirection) {
		base.FixedUpdate ();
	}
}

