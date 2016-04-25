using UnityEngine;
using System.Collections;

/**
 * A stop that can stop certain sliding
 */
public class FrictionTerrain : TerrainPiece
{
	public override string getTerrainType() {
		return "friction terrain";
	}
}

