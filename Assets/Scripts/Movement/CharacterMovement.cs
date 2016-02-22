using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
	public CharacterMovement() {
		print ("Doooo something");
	}
	// player and NPC movement should share this so any calls between the two can be handled easily
	public DefaultMovement npcMovement;
	public virtual void walk() {}
}

