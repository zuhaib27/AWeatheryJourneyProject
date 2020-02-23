using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Player.Scripts;

public class OptionsMenu : Menu
{
    public GameObject player;
    private MyPlayer _player;

    private void Start()
    {
        _player = player.GetComponent<MyPlayer>();
    }

    public void SetInvertCameraY(bool invert)
    {
        PlayerSettings settings = _player.Settings;
        settings.invertCameraY = invert;
        _player.Settings = settings;
    }
}
