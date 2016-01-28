using UnityEngine;
using System.Collections;

public class NpcCombatManager : MonoBehaviour
{
	public string npcType;
	private AttackScript attackAI;
	
	// this will be available to be seen and will be interchangeable
	// between enemies. this allows quick swapping of AI
	
	// see if in range for attack
	// take damage
	// deal damage

	// Use this for initialization
	void Start ()
	{
		switch(npcType) {
		case "Minion":
			attackAI = new MinionAttackScript();
			break;
		case "Q":
			break;
		case "W":			
			break;
		case "E":			
			break;
		case "R":			
			break;
		default:
			break;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		attackAI.test();
	}
}

