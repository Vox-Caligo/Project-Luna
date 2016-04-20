using UnityEngine;
using System.Collections;

/**
 * Special pieces of land that effect the player.
 * Turning on certain values causes different effects
 * to become active.
 */
public class TerrainPiece : MonoBehaviour
{
	// variables for effects
	public bool isSlippery;			// causes sliding
	public bool needsFrictionStop;	// the player will need to hit a friction spot to stop
	public bool isFrictionStop;		// a stop that can stop certain sliding
	public bool isSturdy;			// when collided with it allows player to move other directions when sliding
	public bool climable;			// is a 'climable' object (causes vertical movement always)

	// teleporter items
	public int teleporterGroup;					// group number
	public bool sender = false;					// sends to a teleport
	public bool receiver = false;				// receives from a teleport
	public bool isDirectionalTeleport;			// sends a certain direction when exited
	public int teleportDirection;				// the direction the character is sent
	private TerrainPiece sisterTeleport;		// the other teleport connector
	private bool teleporterOnFreeze = false;	// cannot teleport until it is false

	// speed manipulators
	public bool isSlowdown;				// the piece causes slower movement
	public float slowdownSpeed = .5f;	// how slow the player is slowed down
	public bool isSpeedup;				// the piece causes faster movement
	public float speedupSpeed = 2f;		// how fast the player is sped up

	// where the player will be teleporter, also turns the receiver off so it can't cause
	// infinite teleports
	public Vector2 teleportCoordinates() {
		sisterTeleport.TeleporterOnFreeze = true;
		return sisterTeleport.gameObject.transform.position;
	}

	// if it is a teleporter, finds it's sister teleport
	private void Update() {
		if(sisterTeleport == null) {
			foreach(GameObject possibleSisterTeleporter in GameObject.FindGameObjectsWithTag("Environment")) {
				TerrainPiece sisterBeingChecked = possibleSisterTeleporter.GetComponent<TerrainPiece>();
				if(sisterBeingChecked != null && sisterBeingChecked.teleporterGroup == teleporterGroup &&
					sisterBeingChecked.gameObject != this.gameObject && (sisterBeingChecked.sender || sisterBeingChecked.receiver)) {
					sisterTeleport = sisterBeingChecked.GetComponent<TerrainPiece>();
				}
			}
		}
	}

	// checks if the sister is a reciever
	public bool isSisterAReceiver() {
		return sisterTeleport.receiver;
	}

	// checks if the sister causes directional movement
	public int isSisterADirectional() {
		if(sisterTeleport.isDirectionalTeleport) {
			return sisterTeleport.teleportDirection;
		} 
		return -1;
	}

	// checks if the teleporter can be used
	public bool TeleporterOnFreeze {
		get {return teleporterOnFreeze;}
		set {teleporterOnFreeze = value;}
	}
}
