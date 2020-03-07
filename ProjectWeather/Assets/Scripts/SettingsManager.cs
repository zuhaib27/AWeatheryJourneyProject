using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Assets.Player.Scripts;

public struct PlayerSettings
{
    public bool invertCameraY;
    public float cameraSensitivity;
}

public struct GameSettings
{
    public PlayerSettings playerSettings;

    public float gameVolume;
    public float musicVolume;
}

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private GameSettings _settings;
    public GameSettings Settings { get { return _settings; } }

    public AudioMixer masterAudio;
    
    void Awake()
    {
        #region singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion

        LoadGameSettings();
    }

    private void Start()
    {
        SetPlayerInvertCameraY(_settings.playerSettings.invertCameraY);
        SetPlayerCameraSensitivty(_settings.playerSettings.cameraSensitivity);
        
        SetMasterVolume(_settings.gameVolume);
        SetMusicVolume(_settings.musicVolume);
    }

    #region loading and saving
    private void OnApplicationQuit()
    {
        if (Instance == this)
        {
            SaveGameSettings();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SaveGameSettings();
            Instance = null;
        }
    }

    private void LoadGameSettings()
    {
        _settings.playerSettings.invertCameraY = PlayerPrefs.GetInt("PlayerInvertCameraY", 0) > 0;
        _settings.playerSettings.cameraSensitivity = PlayerPrefs.GetFloat("PlayerCameraSensitivity", .5f);
        _settings.gameVolume = PlayerPrefs.GetFloat("MasterVolume", 0f);
        _settings.musicVolume = PlayerPrefs.GetFloat("MusicVolume", -20f);
    }

    private void SaveGameSettings()
    {
        PlayerPrefs.SetInt("PlayerInvertCameraY", _settings.playerSettings.invertCameraY ? 1 : 0);
        PlayerPrefs.SetFloat("PlayerCameraSensitivity", _settings.playerSettings.cameraSensitivity);
        PlayerPrefs.SetFloat("MasterVolume", _settings.gameVolume);
        PlayerPrefs.SetFloat("MusicVolume", _settings.musicVolume);
    }
    #endregion

    public void SetPlayerInvertCameraY(bool value)
    {
        _settings.playerSettings.invertCameraY = value;

        PlayerInputs playerInputs = FindObjectOfType<PlayerInputs>();
        if (playerInputs != null)
            playerInputs.Settings = _settings.playerSettings;
    }

    public void SetPlayerCameraSensitivty(float value)
    {
        _settings.playerSettings.cameraSensitivity = value;

        PlayerInputs playerInputs = FindObjectOfType<PlayerInputs>();
        if (playerInputs != null)
            playerInputs.Settings = _settings.playerSettings;
    }

    public void SetMasterVolume(float value)
    {
        Instance._settings.gameVolume = value;
        masterAudio.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        _settings.musicVolume = value;
        masterAudio.SetFloat("MusicVolume", value);
    }
}
