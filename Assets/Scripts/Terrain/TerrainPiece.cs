using UnityEngine;
using System.Collections;

/**
 * Special pieces of land that effect the player.
 * Turning on certain values causes different effects
 * to become active.
 */
public class TerrainPiece : MonoBehaviour {
	public virtual string getTerrainType() {
		return null;
	}
}
