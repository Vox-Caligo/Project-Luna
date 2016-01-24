using UnityEngine;
using System.Collections;

public class StableUI : MonoBehaviour {
	static bool spawned = false;
	
	void Awake() {
		// Do not destroy this game object:
		if (spawned)
			DestroyImmediate (gameObject);
		else {
			DontDestroyOnLoad(this);
				spawned = this;
		}
	}
}
