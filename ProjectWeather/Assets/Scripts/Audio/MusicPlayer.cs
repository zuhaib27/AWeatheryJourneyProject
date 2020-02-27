using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    public AudioMixerGroup mixerGroup;

    [Range(0f, 5f)]
    public float delayBetweenSongs = 1;
    [Range(0f, 5f)]
    public float fadeTimeOnSongEnd = 1f;

    public bool loopOverPlaylist = true;
    public bool switchThemeOnAbilityChange = true;

    public Song[] playlist;

    private int _currentTrack = 0;
    private AudioSource _source;

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
        _source = gameObject.AddComponent<AudioSource>();
        _source.outputAudioMixerGroup = mixerGroup;
        PlaySong(_currentTrack);
    }
    
    void Update()
    {
        if (!_source.isPlaying)
        {
            if (loopOverPlaylist)
                _currentTrack = (_currentTrack + 1) % playlist.Length;

            PlaySong(_currentTrack);
        }
    }

    #region fading songs
    IEnumerator WaitForFadeOut(float waitSeconds)
    {
        yield return new WaitForSecondsRealtime(waitSeconds);
        StartCoroutine(FadeOutSong(fadeTimeOnSongEnd));
    }

    public IEnumerator FadeOutSong(float fadeTime)
    {
        while (_source.volume > 0f)
        {
            _source.volume -= (.03f / fadeTime) * playlist[_currentTrack].volume;
            yield return new WaitForSecondsRealtime(.03f);
        }

        _source.Stop();
    }
    #endregion

    public void PlaySong(int index)
    {
        StopAllCoroutines();

        Song song = playlist[index];
        _source.clip = song.clip;
        _source.volume = song.volume;
        _source.time = 0f;
        _source.PlayDelayed(delayBetweenSongs);

        StartCoroutine(WaitForFadeOut(_source.clip.length - fadeTimeOnSongEnd));
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
            float currentTime = _source.time;
            Song song = playlist[_currentTrack];
            _source.clip = song.clip;
            _source.volume = song.volume;
            _source.time = currentTime;
            _source.Play();
        }
    }
}
