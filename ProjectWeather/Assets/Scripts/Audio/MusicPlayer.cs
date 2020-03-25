using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    public AudioMixerGroup mixerGroup;
    
    public float fadeTimeBetweenSongs = 1f;

    public bool loopOverPlaylist = true;
    public bool switchThemeOnAbilityChange = true;

    public Song[] playlist;

    private int _currentTrack = 0;
    private float _volume;

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
    }
    
    void Start()
    {
        foreach (Song s in playlist)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = true;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }

        if (CheckpointManager.Instance)
            CheckpointManager.Instance.OnSpawn += OnSpawn;

        OnSpawn();
    }
    
    void Update()
    {
        if (!playlist[_currentTrack].source.isPlaying)
        {
            if (loopOverPlaylist)
                _currentTrack = (_currentTrack + 1) % playlist.Length;

            PlaySong(_currentTrack);
        }
    }

    private void OnSpawn()
    {
        StopAllCoroutines();

        foreach (Song s in playlist)
        {
            s.source.Stop();
            s.source.Play();
        }

        _volume = SettingsManager.Instance.Settings.musicVolume;
        mixerGroup.audioMixer.SetFloat("MasterVolume", _volume);

        _currentTrack = 0;
        PlaySong(_currentTrack);
    }

    #region fading songs
    //IEnumerator WaitForFadeOut(float waitSeconds)
    //{
    //    yield return new WaitForSecondsRealtime(waitSeconds);
    //    StartCoroutine(FadeOutSong(fadeTimeOnSongEnd));
    //}

    public void FadeOutSong(float fadeTime)
    {
        StartCoroutine(FadeOutSongCoroutine(fadeTime));
    }

    private IEnumerator FadeOutSongCoroutine(float fadeTime)
    {
        float initialVolumePositive = SettingsManager.Instance.Settings.musicVolume + 80f;

        for (float t = 0f; t < 1f; t += .1f / fadeTime)
        {
            _volume = -80f + initialVolumePositive - t * t * initialVolumePositive;
            mixerGroup.audioMixer.SetFloat("MasterVolume", _volume);
            yield return new WaitForSecondsRealtime(.1f);
        }
    }
    #endregion

    public void PlaySong(int index)
    {
        _currentTrack = index;
        Song song = playlist[_currentTrack];

        song.mixerSnapshot.TransitionTo(fadeTimeBetweenSongs);

        //StartCoroutine(WaitForFadeOut(song.source.clip.length - fadeTimeOnSongEnd));
    }

    public void SkipSong()
    {
        _currentTrack = (_currentTrack + 1) % playlist.Length;
        PlaySong(_currentTrack);
    }

    public string GetCurrentSongTitle()
    {
        return playlist[_currentTrack].name;
    }

    public void SwitchSong(int index)
    {
        if (switchThemeOnAbilityChange)
        {
            _currentTrack = index;
            PlaySong(_currentTrack);
        }
    }
}
