using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerJumpSound : MonoBehaviour
{
    public float jumpVolume = 0.25f;
    public AudioClip jumpAudioClip;
    public AudioMixerGroup audioMixerGroup;
    private AudioSource jumpAudioSource;

    private SamuraiMove samuraiMove;

    public void Awake()
    {
        if(jumpAudioSource == null)
        {
            jumpAudioSource = this.gameObject.AddComponent<AudioSource>();
            jumpAudioSource.clip = jumpAudioClip;
            jumpAudioSource.volume = jumpVolume;
            jumpAudioSource.outputAudioMixerGroup = audioMixerGroup;
        }
        if(samuraiMove == null)
            samuraiMove = this.gameObject.GetComponent<SamuraiMove>();
    }

    public void Update()
    {
        if ((samuraiMove.state == SamuraiMove.MovementState.walking) && Input.GetButtonDown("Jump"))
            jumpAudioSource.Play();
    }
}
