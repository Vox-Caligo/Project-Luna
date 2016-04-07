using UnityEngine;
using System.Collections;

public class UtilTimer : MonoBehaviour
{
	private float runningTimerTick;
	private float runningTimerMax;

	public UtilTimer(float timerTick, float timerMax) {
		runningTimerTick = timerTick;
		runningTimerMax = timerMax;
	}

	public bool runningTimerCountdown() {
		if(runningTimerTick > 0) {
			runningTimerTick -= Time.deltaTime;
			return true;
		}

		runningTimerTick = runningTimerMax;
		return false;
	}
}

