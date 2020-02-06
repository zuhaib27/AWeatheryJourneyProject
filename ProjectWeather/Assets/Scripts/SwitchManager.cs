using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwitchManager : MonoBehaviour
{
    public event Action<Weather> AbilitySwitch;

    public GameObject player;

    private PlayerAbility _playerAbility;

    #region Singleton

    public static SwitchManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        _playerAbility = player.GetComponentInChildren<PlayerAbility>();
    }

    // Notifies all subscribed switches that a switch was activated
    public void Notify(Weather weather)
    {
        AbilitySwitch?.Invoke(weather);

        _playerAbility.ActivateAbility(weather);
    }
}
