using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketAndRope : Interactible
{
    public AudioSource fillSound;

    Animator _animator;
    RisingWater _risingWater;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _risingWater = GetComponentInChildren<RisingWater>();

        _animator.speed = .2f;
    }

    private void Update()
    {
        //_animator.SetVector("", )
        if (_risingWater.DidWaterLevelChange())
        {
            _animator.enabled = true;

            if (fillSound && !fillSound.isPlaying)
                fillSound.Play();

            fillSound.pitch += 1f / _animator.GetCurrentAnimatorStateInfo(0).length * _animator.speed * Time.deltaTime;
        }
        else
        {
            _animator.enabled = false;
            fillSound.Stop();
        }
    }
}
