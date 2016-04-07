using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerKeys {
	public bool Pressed { get; set; }
	public bool Used { get; set; }

	public PlayerKeys(bool pressed, bool used) {
		this.Pressed = pressed;
		this.Used = used;
	}
}

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
		{KeyCode.UpArrow, new PlayerKeys(false, false)},
		{KeyCode.LeftArrow, new PlayerKeys(false, false)},
		{KeyCode.DownArrow, new PlayerKeys(false, false)},
		{KeyCode.RightArrow, new PlayerKeys(false, false)},
		{KeyCode.Space, new PlayerKeys(false, false)}
	};

	public bool useKey(KeyCode pressedKey) {
		PlayerKeys checkingKeyProperties = usedKeys [pressedKey];
		if (checkingKeyProperties.Pressed && !checkingKeyProperties.Used) {
			usedKeys [pressedKey] = new PlayerKeys (checkingKeyProperties.Pressed, true);
			return true;
		}
		return false;
	}

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

