using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public CustomeSounds[] sounds;
    // Start is called before the first frame update
    private void Awake()
    {
        foreach (CustomeSounds s in sounds) {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }
    public void PlayAudio(string name) {
        CustomeSounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
