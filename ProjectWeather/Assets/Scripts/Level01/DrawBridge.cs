using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBridge : MonoBehaviour
{
    Animator _animator;

    public float baseDrawSpeed = 1;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = 0f;
    }

    public void SetDrawSpeed(float speed)
    {
        speed = Mathf.Clamp(speed, 0f, 1f);
        _animator.speed = baseDrawSpeed * speed;
    }
}
