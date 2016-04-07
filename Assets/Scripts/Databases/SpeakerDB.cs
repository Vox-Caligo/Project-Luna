using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeakerDB : MonoBehaviour
{
	private Dictionary<string, string> allSpeakers;
	private Dictionary<string, string> allProfiles;

	void Start() {
		allSpeakers = new Dictionary<string, string> ();
		allSpeakers.Add ("Aegis", "test1");
		allSpeakers.Add ("Minion", "test2");

		allProfiles = new Dictionary<string, string>();
		allProfiles.Add("Aegis", "TestProfile");
	}

	public string getSpeaker(string character) {
		if(allProfiles.ContainsKey(character)) {
			return allProfiles[character];
		}

		return "Character does not exist in Character Database";
	}

	public string getProfile(string character) {
		if(allProfiles.ContainsKey(character)) {
			return allProfiles[character];
		}

		return "Character Profile does not exist in Character Database";
	}
}

