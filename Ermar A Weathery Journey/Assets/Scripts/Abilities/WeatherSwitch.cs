using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSwitch : MonoBehaviour
{
    public Weather weather;

    private readonly float _compressionFactor = 0.5f;

    private Collider _collider;
    private SwitchManager _switchManager;

    private void Start()
    {
        _collider = GetComponent<Collider>();
        _switchManager = FindObjectOfType<SwitchManager>();

        // Subscribe to switch events
        _switchManager.AbilitySwitch += OnAbilitySwitch;
    }

    // Called when ability is switched
    private void OnAbilitySwitch(Weather weatherSwitchedTo)
    {
        if (!_collider.isTrigger && weatherSwitchedTo != weather)
        {
            transform.Translate(transform.up * transform.localScale.y * _compressionFactor);
            _collider.isTrigger = true;
        }
    }

    // Called on trigger event
    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Contains("Static Environment"))
        {
            Debug.Log("Activating " + weather);
            _collider.isTrigger = false;

            transform.Translate(-transform.up * transform.localScale.y * _compressionFactor);

            // Notify all other switches
            _switchManager.Notify(weather);
        }
    }
}
