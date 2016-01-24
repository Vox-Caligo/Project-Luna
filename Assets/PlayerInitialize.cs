using UnityEngine;
using System.Collections;

public class PlayerInitialize : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		Instantiate(player);
	}
}
