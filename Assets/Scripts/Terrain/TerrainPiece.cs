using UnityEngine;
using System.Collections;

public class TerrainPiece : MonoBehaviour
{
	public bool isSlippery;
	public bool isSlowdown;
	public float slowdownSpeed = .5f;
	public bool isSpeedup;
	public float speedupSpeed = 2f;
	// piece and sister piece (teleporting and what not) - won't respond until player steps off or something
}
