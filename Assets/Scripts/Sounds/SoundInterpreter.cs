using UnityEngine;
using System.Collections;

public class SoundInterpreter : MonoBehaviour {
	private AudioSource source;
	private AudioClip currentSound;
	
	void Start() {
		source = Camera.main.audio;
	}
	
	public void playOneShot(string newSound){
		source = Camera.main.audio;
		
		switch(newSound) {
		case "slashing attack":
			currentSound = Resources.Load<AudioClip>("Sound Effects/Attacks/Melee/Slashing/Slashing1");
			break;
		case "fire attack":
			currentSound = Resources.Load<AudioClip>("Sound Effects/Attacks/Magic/Fire/Fireball" + Random.Range(1,3));
			break;
		case "slashing":
			break;
		default:
			currentSound = Resources.Load<AudioClip>("error");
			break;
		}
		
		source.PlayOneShot(currentSound);
	}
	
	public void playMusic(string musicTitle, bool repeat){
		source = Camera.main.audio;
		
		switch(musicTitle) {
		case "Title":
			source.clip = Resources.Load<AudioClip>("Music/Trixie's Trix (Short Edit)");
			break;
		case "Ghost World":
			source.clip = Resources.Load<AudioClip>("Music/GhostWorld");
			break;
		case "slashing":
			break;
		default:
			source.clip = Resources.Load<AudioClip>("error");
			break;
		}
		
		source.Play();
		
		if(repeat) {
			source.loop = true;
		} else {
			source.loop = false;
		}
	}
}
