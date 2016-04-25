using UnityEngine;
using System.Collections;

/**
 * Terrain pieces that manipulates character health
 */
public class HealthManipulatorTerrain : TerrainPiece
{
	public int healthManipulation = 1;
	public bool overboost = false;

	public override string getTerrainType() {
		return "health manipulator terrain";
	}
}

