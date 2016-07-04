using UnityEngine;
using System.Collections;

public class DamageArea : MonoBehaviour
{
    public string parent;
    public WeaponStats weapon;

    // applies damage to the enemy being hit (does so by checking stats vs enemy defenses)
    protected void OnCollisionEnter2D(Collision2D col) {
        GameObject target = col.gameObject;

        if (parent != target.name) {
            if (target.GetComponent<MasterBehavior>() != null) {
                print("Hitting " + target.name + " with health " + target.GetComponent<MasterBehavior>().characterHealth());
                target.GetComponent<MasterBehavior>().characterHealth(target.GetComponent<MasterBehavior>().characterHealth() - weapon.Damage);
                target.GetComponent<MasterBehavior>().characterInCombat();
            }

            // can't just run through with a weapon (currently)
            Destroy(this.gameObject);
        }
    }

	protected virtual void Update () { }
}

