using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Player.Scripts;
using UnityEngine.UI;

public class Options: MonoBehaviour
{
    public Toggle invertCameraY;
    public Slider cameraSensitivitySlider;

    public Slider gameVolumeSlider;
    public Slider musicVolumeSlider;

    private void Start()
    {
        #region dependency check
        if (!SettingsManager.Instance)
        {
            Debug.LogError("Could not find SettingsManager in scene.");
        }
        #endregion

        invertCameraY.isOn = SettingsManager.Instance.Settings.playerSettings.invertCameraY;
        cameraSensitivitySlider.value = SettingsManager.Instance.Settings.playerSettings.cameraSensitivity;

        gameVolumeSlider.value = SettingsManager.Instance.Settings.sfxVolume;
        musicVolumeSlider.value = SettingsManager.Instance.Settings.musicVolume;
    }

    public void SetInvertCameraY(bool value)
    {
        SettingsManager.Instance.SetPlayerInvertCameraY(value);
    }

    public void SetCameraSensitivity(float value)
    {
        SettingsManager.Instance.SetPlayerCameraSensitivty(value);
    }

    public void SetSFXVolume(float value)
    {
        SettingsManager.Instance.SetSFXVolume(value);
    }

    public void SetMusicVolume(float value)
    {
        SettingsManager.Instance.SetMusicVolume(value);
    }
}
