using UnityEngine;
using System.Collections;

public class AudioPiece : MonoBehaviour {

    void Start() {
        AudioSource currentAudio = gameObject.GetComponent<AudioSource>();
        Invoke("removeAudioPiece", currentAudio.clip.length);
    }

    private void removeAudioPiece() {
        Destroy(this.gameObject);
    }
}
