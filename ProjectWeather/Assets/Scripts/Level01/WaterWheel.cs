using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheel : MonoBehaviour
{
    public float acceleration = .5f;
    public float maxWheelSpeed = 1f;
    public float soundSpeed = 1f;

    public DrawBridge drawBridge;

    private bool _isRain = false;
    private float _currentSpeed = 0f;
    private float _distanceSincePlayedSound = 0f;

    private Animator _animator;

    private AudioSource _crankSound;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _crankSound = GetComponent<AudioSource>();
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

        // Animations
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxWheelSpeed);
        _animator.speed = _currentSpeed;
        drawBridge.SetDrawSpeed( _currentSpeed / maxWheelSpeed);

        // Sound
        if (_distanceSincePlayedSound > 1f / soundSpeed)
        {
            _crankSound.Play();
            _distanceSincePlayedSound = 0f;
        }
        _distanceSincePlayedSound += _currentSpeed * Time.deltaTime;

        _isRain = false;
    }

    public void OnRain()
    {
        _isRain = true;
    }
}
