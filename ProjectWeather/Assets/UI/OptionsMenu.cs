using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Player.Scripts;
using UnityEngine.UI;

public class OptionsMenu : Menu
{
    public Toggle invertCameraY;

    public Slider gameVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start()
    {
        invertCameraY.isOn = SettingsManager.Instance.Settings.playerSettings.invertCameraY;

        gameVolumeSlider.value = SettingsManager.Instance.Settings.gameVolume;
        musicVolumeSlider.value = SettingsManager.Instance.Settings.musicVolume;
    }

    public void SetInvertCameraY(bool value)
    {
        SettingsManager.Instance.SetPlayerInvertCameraY(value);
    }

    public void SetGameVolume(float value)
    {
        SettingsManager.Instance.SetMasterVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        SettingsManager.Instance.SetMusicVolume(value);
    }
}
