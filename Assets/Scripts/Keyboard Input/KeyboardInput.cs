using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * When used, checks if a key has been pressed and
 * if that press has caused it to be used for anything.
 */
public class PlayerKeys {
	public bool Pressed { get; set; }
	public bool Used { get; set; }

	public PlayerKeys(bool pressed, bool used) {
		this.Pressed = pressed;
		this.Used = used;
	}
}

/**
 * A better way to check if keys have been pressed and used.
 * Stores it in a way that makes retrieval of information easier
 */
public class KeyboardInput : MonoBehaviour
{
	// checks if key is pressed and changes the first value if so. The second value
	// checks whether it has been engaged or not
	private Dictionary<KeyCode, PlayerKeys> usedKeys = new Dictionary<KeyCode, PlayerKeys>() {
		{KeyCode.E, new PlayerKeys(false, false)},
		{KeyCode.W, new PlayerKeys(false, false)},
		{KeyCode.A, new PlayerKeys(false, false)},
		{KeyCode.S, new PlayerKeys(false, false)},
		{KeyCode.D, new PlayerKeys(false, false)},
		{KeyCode.Q, new PlayerKeys(false, false)},
        {KeyCode.R, new PlayerKeys(false, false)},
        {KeyCode.O, new PlayerKeys(false, false)}, // for testing
		{KeyCode.P, new PlayerKeys(false, false)}, // for testing
		{KeyCode.UpArrow, new PlayerKeys(false, false)},
		{KeyCode.LeftArrow, new PlayerKeys(false, false)},
		{KeyCode.DownArrow, new PlayerKeys(false, false)},
		{KeyCode.RightArrow, new PlayerKeys(false, false)},
		{KeyCode.Space, new PlayerKeys(false, false)}
	};

	// uses the key that was pressed (if able)
	public bool useKey(KeyCode pressedKey) {
		PlayerKeys checkingKeyProperties = usedKeys [pressedKey];
		if (checkingKeyProperties.Pressed && !checkingKeyProperties.Used) {
			usedKeys [pressedKey] = new PlayerKeys (checkingKeyProperties.Pressed, true);
			return true;
		}
		return false;
	}

	// checks which keys are down and which are up and resets/sets
	// values appropriately
	void Update () {
		ArrayList modifiedButtons = new ArrayList ();

		foreach(KeyValuePair<KeyCode, PlayerKeys> entry in usedKeys) {
			KeyCode checkKey = entry.Key;
			PlayerKeys checkProperties = entry.Value;

			if ((Input.GetKeyDown (checkKey) && !checkProperties.Pressed) ||
				(Input.GetKeyUp (entry.Key) && checkProperties.Pressed)) {
				modifiedButtons.Add (checkKey);
			}
		}

		foreach (KeyCode modifiedKey in modifiedButtons) {
			PlayerKeys checkProperties = usedKeys[modifiedKey];
			bool isPressed = checkProperties.Pressed ? false : true;
			usedKeys[modifiedKey] = new PlayerKeys(isPressed, false);
		}
	}
}

