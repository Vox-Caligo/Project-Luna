using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerDB : MonoBehaviour
{
	private Dictionary<string, string> allSpeakers;

	void Start() {
		allSpeakers = new Dictionary<string, string> ();
		allSpeakers.Add ("Aegis", "test1");
		allSpeakers.Add ("Minion", "test2");
	}

	public string getSpeaker(string character) {
		if(allSpeakers.ContainsKey(character)) {
			return allSpeakers[character];
		}

		return "Character does not exist in Character Database";
	}
}

