using UnityEngine;
using System.Collections;

/**
 * Terrain that causes characters to slide around
 */
public class SlipperyTerrain : TerrainPiece
{
	public bool needsFrictionStop;	// the player will need to hit a friction spot to stop

	public override string getTerrainType() {
		return "slippery terrain";
	}
}

