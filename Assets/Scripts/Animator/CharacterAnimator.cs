using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour
{
	private Animator animator;

	public CharacterAnimator(GameObject player) {
		animator = player.GetComponent<Animator>();
	}

	public void walk(int currentDirection) {
		animator.SetBool ("Walking", true);
		animator.speed = 1;
		animator.SetInteger ("Direction", currentDirection);
	}

	public void stop() {
		animator.speed = 0;
		animator.SetBool ("Walking", false);
	}

	public bool isInMotion() {
		return animator.GetBool("Walking");
	}
}

