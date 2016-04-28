using UnityEngine;
using System.Collections;

public class WaterTerrain : SpeedManipulatorTerrain
{
	public override string getTerrainType() {
		if (isSlowdown || isSpeedup) {
			return "water terrain";
		}

		return null;
	}
}

