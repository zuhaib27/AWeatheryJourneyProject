using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public float acceleration = .5f;
    public float maxSpeed = 1f;

    public DrawBridge drawBridge;

    private bool _isRain = false;
    private float _currentSpeed = 0f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isRain)
        {
            _currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= acceleration * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxSpeed);
        _animator.speed = _currentSpeed;
        drawBridge.SetDrawSpeed(_currentSpeed / maxSpeed);
        _isRain = false;
    }

    public void OnRain()
    {
        _isRain = true;
    }
}
