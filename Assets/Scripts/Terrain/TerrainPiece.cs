using UnityEngine;
using System.Collections;

public class TerrainPiece : MonoBehaviour
{
	public bool isSlippery;
	public bool needsFrictionStop;
	public bool isFrictionStop;
	public bool isSturdy;
	public bool climable;

	// teleporter items
	public int teleporterGroup;
	public bool sender = false;
	public bool receiver = false;
	public bool isDirectionalTeleport;
	public int teleportDirection;
	private TerrainPiece sisterTeleport;
	private bool teleporterOnFreeze = false;

	// speed manipulators
	public bool isSlowdown;
	public float slowdownSpeed = .5f;
	public bool isSpeedup;
	public float speedupSpeed = 2f;

	public Vector2 teleportCoordinates() {
		// disable sister return function (if applicable, the player will turn it on again
		sisterTeleport.TeleporterOnFreeze = true;
		return sisterTeleport.gameObject.transform.position;
	}

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

	public bool isSisterAReceiver() {
		return sisterTeleport.receiver;
	}

	public int isSisterADirectional() {
		if(sisterTeleport.isDirectionalTeleport) {
			return sisterTeleport.teleportDirection;
		} 
		return -1;
	}

	public bool TeleporterOnFreeze {
		get {return teleporterOnFreeze;}
		set {teleporterOnFreeze = value;}
	}
}
