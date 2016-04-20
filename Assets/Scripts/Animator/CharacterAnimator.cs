using UnityEngine;
using System.Collections;

/**
 * Allows a character to be animated on the overworld screen.
 * This includes walking, running, attacking, and special moves
 * like teleporting and dashing.
 */
public class CharacterAnimator : MonoBehaviour
{
	private Animator animator;	// animator that is called

	// constructor for the character to be animated
	public CharacterAnimator(GameObject player) {
		animator = player.GetComponent<Animator>();
	}

	// used to have a character walk
	public void walk(int currentDirection) {
		animator.SetBool ("Walking", true);
		animator.speed = 1;
		animator.SetInteger ("Direction", currentDirection);
	}

	// used to cause the character to stop being animated
	public void stop() {
		animator.speed = 0;
		animator.SetBool ("Walking", false);
	}

	// check if the character is in motion
	public bool isInMotion() {
		return animator.GetBool("Walking");
	}
}

