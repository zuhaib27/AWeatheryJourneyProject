using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketAndRope : MonoBehaviour
{
    Animator _animator;
    RisingWater _risingWater;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _risingWater = GetComponentInChildren<RisingWater>();
    }

    private void Update()
    {
        //_animator.SetVector("", )
        _animator.enabled = _risingWater.DidWaterLevelChange();
        Debug.Log("Did water level change: " + _risingWater.DidWaterLevelChange());
    }
}
