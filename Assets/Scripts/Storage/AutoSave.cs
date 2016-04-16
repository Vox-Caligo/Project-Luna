using UnityEngine;
using System.Collections;

public class AutoSave : MonoBehaviour
{
	private UtilTimer autoSave;
	private int timeDelay = 150000; // 2.5 min

	public AutoSave() {
		autoSave = new UtilTimer (1, 1); // replace with timeDelay when not testing 
	}

	public bool autoSaveUpdate ()
	{
		if (autoSave.runningTimerCountdown ()) {
			return true;
		}

		return false;
	}
}

