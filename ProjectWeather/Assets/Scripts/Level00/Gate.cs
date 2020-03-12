using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gate : MonoBehaviour
{
    public RisingWater risingWaterTrigger;

    private AudioSource _audioSource;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponentInChildren<AudioSource>();
        risingWaterTrigger.OnReachMaxLevel += OpenGate;
    }

    public void OpenGate()
    {
        _animator.SetBool("IsOpen", true);
        _audioSource.Play();
    }
}
