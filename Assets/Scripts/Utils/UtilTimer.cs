using UnityEngine;
using System.Collections;

/**
 * A clock that is used to countdown before resetting
 * and allowing more countdowns.
 */
public class UtilTimer : MonoBehaviour
{
	// current and max timer
	private float runningTimerTick;
	private float runningTimerMax;

	// sets the current and max timer
	public UtilTimer(float timerTick, float timerMax) {
		runningTimerTick = timerTick;
		runningTimerMax = timerMax;
	}

	// countsdown per second and returns true while the timer is above 0
	public bool runningTimerCountdown() {
		if(runningTimerTick > 0) {
			runningTimerTick -= Time.deltaTime;
			return true;
		}

		runningTimerTick = runningTimerMax;
		return false;
	}
}

