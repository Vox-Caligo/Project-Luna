using UnityEngine;
using System.Collections;

public class SoundInterpreter : MonoBehaviour {
    private GameObject attachedParent;
	private GameObject source;
	private AudioClip currentSound;
    private SoundDB soundDB;
    private ArrayList activeSources = new ArrayList();

    public SoundInterpreter(GameObject attachedParent) {
        this.attachedParent = attachedParent;
        soundDB = GameObject.Find("Databases").GetComponent<SoundDB>();
    }

    // plays a single sound or environmental music depending on the variables given
    public void playSound(string sound, bool oneOff, bool repeat = false) {
        currentSound = Resources.Load<AudioClip>(soundDB.getSound(sound));

        if (oneOff) {
            source = new GameObject();
            source.name = "Active Sound";
            source.transform.parent = attachedParent.transform;
            AudioSource audioSource = source.AddComponent<AudioSource>();
            audioSource.clip = currentSound;
            audioSource.PlayOneShot(currentSound);
            source.AddComponent<AudioPiece>();
            activeSources.Add(source);
        } else {
            AudioSource audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
            audioSource.clip = currentSound;
            audioSource.Play();
            audioSource.loop = repeat;
        }
    }
}
