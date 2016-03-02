using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class Talking : MonoBehaviour
{
	// point for the player to talk
	private Selector reticle;

	// Use this for initialization
	void Start ()
	{
		reticle = this.gameObject.GetComponent<Selector>();
		reticle.CustomPosition = this.gameObject.transform.position;
	}

	void FixedUpdate() {
		reticle.direction = this.gameObject.GetComponent<PlayerMovement>().CurrentDirection;
		reticle.CustomPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
	}
}