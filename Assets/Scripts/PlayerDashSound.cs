using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerDashSound : MonoBehaviour
{
    public float dashVolume = 0.25f;
    public AudioClip dashAudioClip;
    public AudioMixerGroup audioMixerGroup;
    private AudioSource dashAudioSource;

    private SamuraiMove samuraiMove;

    public void Awake()
    {
        if (dashAudioSource == null)
        {
            dashAudioSource = this.gameObject.AddComponent<AudioSource>();
            dashAudioSource.clip = dashAudioClip;
            dashAudioSource.volume = dashVolume;
        }
        if (samuraiMove == null)
            samuraiMove = this.gameObject.GetComponent<SamuraiMove>();
    }

    public void Update()
    {
        if ((samuraiMove.state != SamuraiMove.MovementState.dashing) && Input.GetButtonDown("Fire3"))
            dashAudioSource.Play();
    }
}
