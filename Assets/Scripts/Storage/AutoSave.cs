using UnityEngine;
using System.Collections;

/**
 * Used to save player progress automatically
 */
public class AutoSave : MonoBehaviour
{
	// creates a timer to loop
	private UtilTimer autoSave;
	private int timeDelay = 150000; // 2.5 min

	// sets the timer to be looped
	public AutoSave() {
		autoSave = new UtilTimer (1, 1); // replace with timeDelay when not testing 
	}

	// checks if the timer has hit and returns a bool if appropriate
	public bool autoSaveUpdate ()
	{
		if (autoSave.runningTimerCountdown ()) {
			return true;
		}

		return false;
	}
}

