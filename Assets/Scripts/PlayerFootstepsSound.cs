using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerFootsteps : MonoBehaviour
{
    public float footstepVolume = 0.25f;
    public float timeBetweenStepSound;
    private float timeLeftBetweenStepSound;

    public AudioClip footstepAudioClip;
    public AudioMixerGroup mixerGroup;

    private AudioSource footstepAudioSource;
    private SamuraiMove samuraiMove;
    private Rigidbody rb;

    private float VelocityThreshold = 4.0f;

    public void Awake()
    {
        if(footstepAudioSource == null)
        {
            footstepAudioSource = this.gameObject.AddComponent<AudioSource>();
            if(footstepAudioClip != null)
                footstepAudioSource.clip = footstepAudioClip;
            footstepAudioSource.volume = footstepVolume;
            footstepAudioSource.outputAudioMixerGroup = mixerGroup;
        }
        if(samuraiMove == null)
        {
            samuraiMove = this.gameObject.GetComponent<SamuraiMove>();
            rb = samuraiMove.GetComponent<Rigidbody>();
        }

        timeLeftBetweenStepSound = timeBetweenStepSound;
    }

    public void Update()
    {
        if (samuraiMove != null)
        {
            timeLeftBetweenStepSound -= Time.deltaTime;
            if (samuraiMove.state == SamuraiMove.MovementState.walking && (!footstepAudioSource.isPlaying))
            {
                if(Mathf.Abs(rb.velocity.x) > VelocityThreshold ||
                   Mathf.Abs(rb.velocity.y) > VelocityThreshold ||
                   Mathf.Abs(rb.velocity.z) > VelocityThreshold )
                {
                    if (timeLeftBetweenStepSound <= 0)
                    {
                        footstepAudioSource.Play();
                        timeLeftBetweenStepSound = timeBetweenStepSound;
                    }
                }
                else
                {
                    footstepAudioSource.Pause();
                    footstepAudioSource.time = 0;
                }
            }
        }
    }
}
