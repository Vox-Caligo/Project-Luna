using UnityEngine;
using System.Collections;

public class TerrainPiece : BaseTerrain
{
	public bool isSlippery;
	public bool needsFrictionStop;
	public bool isFrictionStop;
	public bool isSturdy;
	public bool isSlowdown;
	public float slowdownSpeed = .5f;
	public bool isSpeedup;
	public float speedupSpeed = 2f;
}
