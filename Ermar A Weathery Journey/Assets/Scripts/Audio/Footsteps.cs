using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Footsteps : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public float playSpeed = 1f;

    public Sound audioFloorA;
    public Sound audioFloorB;
    public Sound audioFloorC;
    public Sound audioFloorD;

    private AudioSource _source;
    private float _timeLastPlayed;

    private Assets.Player.Scripts.MyCharacterController _characterController;
    
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.spatialBlend = 1f;
        _source.outputAudioMixerGroup = mixerGroup;
        _timeLastPlayed = Time.time;

        _characterController = GetComponentInChildren<Assets.Player.Scripts.MyCharacterController>();
    }
    
    void Update()
    {
        bool playerIsGrounded = _characterController.IsPlayerOnGround();
        float playerVelocity = _characterController.GetPlayerCurrentVelocity();

        if (playerIsGrounded && playerVelocity > 0f)
        {
            if (Time.time - _timeLastPlayed > 1f / (playerVelocity * playSpeed))
            {
                Sound s = GetCurrentStepSound();
                _source.Stop();
                _source.clip = s.clip;
                _source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance, s.volumeVariance));
                _source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance, s.pitchVariance));
                _source.Play();
                _timeLastPlayed = Time.time;
            }
        }
        else
        {
            _source.Stop();
        }
    }

    private Sound GetCurrentStepSound()
    {
        return audioFloorA;
    }
}
