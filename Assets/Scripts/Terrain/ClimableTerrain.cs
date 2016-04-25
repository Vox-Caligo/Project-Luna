using UnityEngine;
using System.Collections;

/**
 * Terrain that causes vertical movement constantly
 */
public class ClimableTerrain : TerrainPiece
{
	public override string getTerrainType() {
		return "climable terrain";
	}
}

