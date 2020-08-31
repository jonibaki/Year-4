using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public void SetVolume(float vol) {
        audioMixer.SetFloat("volume",vol);

    }
}
