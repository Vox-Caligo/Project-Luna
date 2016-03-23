using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour
{
	public virtual void onInteraction() { }
	public virtual void onInteractionWithMovable(PlayerMovement player) { }
}

