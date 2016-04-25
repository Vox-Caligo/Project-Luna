using UnityEngine;
using System.Collections;

/**
 * A teleporter
 */
public class TeleporterTerrain : TerrainPiece
{
	// teleporter items
	public int teleporterGroup;					// group number
	public bool sender = false;					// sends to a teleport
	public bool receiver = false;				// receives from a teleport
	public bool isDirectionalTeleport;			// sends a certain direction when exited
	public int teleportDirection;				// the direction the character is sent
	private TeleporterTerrain sisterTeleport;	// the other teleport connector
	private bool teleporterOnFreeze = false;	// cannot teleport until it is false

	public override string getTerrainType() {
		return "teleporter terrain";
	}

	// if it is a teleporter, finds it's sister teleport
	private void Update() {
		if(sisterTeleport == null) {
			foreach(GameObject possibleSisterTeleporter in GameObject.FindGameObjectsWithTag("Environment")) {
				TeleporterTerrain sisterBeingChecked = possibleSisterTeleporter.GetComponent<TeleporterTerrain>();
				if(sisterBeingChecked != null && sisterBeingChecked.teleporterGroup == teleporterGroup &&
					sisterBeingChecked.gameObject != this.gameObject && (sisterBeingChecked.sender || sisterBeingChecked.receiver)) {
					sisterTeleport = sisterBeingChecked.GetComponent<TeleporterTerrain>();
				}
			}
		}
	}

	// where the player will be teleporter, also turns the receiver off so it can't cause
	// infinite teleports
	public Vector2 teleportCoordinates() {
		sisterTeleport.TeleporterOnFreeze = true;
		return sisterTeleport.gameObject.transform.position;
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

