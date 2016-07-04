using UnityEngine;
using System.Collections;

public class MeleeAttackArea : DamageArea {
    private bool setup = false;
    public UtilTimer swingTimer;

	protected override void Update ()
	{
        if(!setup) {
            setup = true;
            swingTimer = new UtilTimer(weapon.Speed, weapon.Speed);
        }

        swingTimer.runningTimerCountdown();
        
		if (swingTimer.runningTimerCountdown() /*&& slashing*/) {
            weaponMovement();	
		} else {
			Destroy(this.gameObject);
		}
	}

    private void weaponMovement() {
        
    }
}

