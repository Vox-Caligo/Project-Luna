using UnityEngine;
using System.Collections;

/**
 * Terrain pieces that manipulate the speed of 
 * characters on them
 */
public class SpeedManipulatorTerrain : TerrainPiece
{
	public bool isSlowdown;				// the piece causes slower movement
	public float slowdownSpeed = .5f;	// how slow the player is slowed down
	public bool isSpeedup;				// the piece causes faster movement
	public float speedupSpeed = 2f;		// how fast the player is sped up

	public override string getTerrainType() {
		if (isSlowdown || isSpeedup) {
			return "speed manipulator terrain";
		}

		return null;
	}
}

