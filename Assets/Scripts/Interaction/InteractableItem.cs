using UnityEngine;
using System.Collections;

/**
 * An object that can be interacted with.
 */
public class InteractableItem : MonoBehaviour
{
	public virtual void onInteraction() { }
	public virtual void onInteractionWithMovable(PlayerMovement player) { }
}

