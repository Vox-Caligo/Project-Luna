using UnityEngine;
using System.Collections;

public class Teleporter : BaseTerrain
{
	public int teleporterGroup;
	public bool sender;
	public bool receiver;
	private Teleporter sisterTeleport;
	private bool teleporterOnFreeze = false;

	public Teleporter () {	
		getSisterTeleport();
	}

	private void getSisterTeleport() {	}

	public Vector2 teleportCoordinates() {
		// disable sister return function (if applicable, the player will turn it on again
		sisterTeleport.TeleporterOnFreeze = true;
		return sisterTeleport.gameObject.transform.position;
	}

	private void Update() {
		if(sisterTeleport == null) {
			foreach(GameObject possibleSisterTeleporter in GameObject.FindGameObjectsWithTag("Environment")) {
				Teleporter sisterBeingChecked = possibleSisterTeleporter.GetComponent<Teleporter>();

				if(sisterBeingChecked != null && sisterBeingChecked.teleporterGroup == teleporterGroup &&
					sisterBeingChecked.gameObject != this.gameObject) {
					sisterTeleport = sisterBeingChecked.GetComponent<Teleporter>();
				}
			}
		}
	}

	public bool isSisterAReceiver() {
		return sisterTeleport.receiver;
	}

	public bool TeleporterOnFreeze {
		get {return teleporterOnFreeze;}
		set {teleporterOnFreeze = value;}
	}
}

