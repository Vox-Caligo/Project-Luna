using UnityEngine;
using System.Collections;

public class DefaultColliderInterpreter : MonoBehaviour
{
	private bool hostile;

	public DefaultColliderInterpreter(bool hostile) {
		this.hostile = hostile;
	}

	public string interpretCollision (Collision2D col) {
		// if wall, behave accordingly
		print ("hey");
		if(col.gameObject.tag == "Player")
			return "player";

		return "nothing";
	}

	public bool Hostile {
		get { return hostile; }
	}
}