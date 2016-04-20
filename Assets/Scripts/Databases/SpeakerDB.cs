using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * (Put on Database GameObject) Returns images of the 
 * characters for either speaking or profile pictures
 */
public class SpeakerDB : MonoBehaviour
{
	private Dictionary<string, string> allSpeakers;
	private Dictionary<string, string> allProfiles;

	// sets the dictionary
	void Awake() {
		allSpeakers = new Dictionary<string, string> ();
		allSpeakers.Add ("Aegis", "test1");
		allSpeakers.Add ("Minion", "test2");

		allProfiles = new Dictionary<string, string>();
		allProfiles.Add("Aegis", "TestProfile");
	}

	// returns the picture of the character speaking
	public string getSpeaker(string character) {
		if(allProfiles.ContainsKey(character)) {
			return allProfiles[character];
		}

		return "Character does not exist in Character Database";
	}

	// returns the profile picture of the character
	public string getProfile(string character) {
		if(allProfiles.ContainsKey(character)) {
			return allProfiles[character];
		}

		return "Character Profile does not exist in Character Database";
	}
}

