using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

public class PlayerCombat : MonoBehaviour
{
	// attack timer
	public bool inAttack = false;
	private float timerTick = .5f;
	private float maxTimer = .5f;
	
	/* Attack Box: 
		void Start ()
		{
			attackArea = new GameObject();
			attackArea.transform.parent = gameObject.transform;
			attackArea.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
		}
	*/
	
	// taking damage
	// dealing damage
	// animation
	
	// applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
	public void attacking(string enemyName) {
		int atkDamage = DialogueLua.GetActorField("Player", "Slashing Damage").AsInt - DialogueLua.GetActorField(enemyName, "Slashing Defence").AsInt;
		DialogueLua.SetActorField(enemyName, "Health", DialogueLua.GetActorField(enemyName, "Health").AsInt - atkDamage);
	}	
	
	void FixedUpdate() {
		if(inAttack) {
			if(timerTick > 0) {
				timerTick -= Time.deltaTime;						
			} else if (timerTick <= 0) {
				inAttack = false;
				timerTick = maxTimer;
				Destroy(GameObject.Find ("Player Attack").GetComponent<BoxCollider2D>());
			}
		}
	}
}

