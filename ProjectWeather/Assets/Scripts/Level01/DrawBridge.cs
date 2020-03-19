using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    Animator _animator;

    public float baseDrawSpeed = 1;

    public AudioSource audioSource;
    private bool _audioHasPlayed = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0f;
    }

    private void Update()
    {
    }
    
    public void SetDrawSpeed(float speed)
    {
        speed = Mathf.Clamp(speed, 0f, 1f);
        _animator.speed = baseDrawSpeed * speed;

        if (_animator.speed > 0 && !_audioHasPlayed)
        {
            audioSource.Play();
            _audioHasPlayed = true;
        }

        audioSource.volume = Mathf.Sqrt(_animator.speed);
    }
}
