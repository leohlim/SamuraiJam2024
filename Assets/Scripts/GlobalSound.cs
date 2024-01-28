using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalSound : MonoBehaviour
{
    public AudioClip ostAudioClip;
    private AudioSource ostAudioSource;
    public AudioMixerGroup ostMixerGroup;

    // Start is called before the first frame update
    void Start()
    {
        if(ostAudioSource == null)
        {
            ostAudioSource = this.gameObject.AddComponent<AudioSource>();
            ostAudioSource.clip = ostAudioClip;
            ostAudioSource.loop = true;
            ostAudioSource.volume = 0.15f;
            ostAudioSource.outputAudioMixerGroup = ostMixerGroup;
        }

        ostAudioSource.Play();
        DontDestroyOnLoad(gameObject);
    }

}
