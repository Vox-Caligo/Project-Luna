using UnityEngine;
using System.Collections;

/**
 * Terrain pieces that manipulates character mana
 */
public class ManaManipulatorTerrain : TerrainPiece
{
	public int manaManipulation = 1;
	public bool overboost = false;

	public override string getTerrainType() {
		return "mana manipulator terrain";
	}
}

