using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }
    
    public float fadeTimeBetweenSongs = 1f;

    public bool loopOverPlaylist = true;
    public bool switchThemeOnAbilityChange = true;

    public Song[] playlist;

    private int _currentTrack = 0;

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
            s.source.Play();
        }

        PlaySong(_currentTrack);
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

    #region fading songs
    //IEnumerator WaitForFadeOut(float waitSeconds)
    //{
    //    yield return new WaitForSecondsRealtime(waitSeconds);
    //    StartCoroutine(FadeOutSong(fadeTimeOnSongEnd));
    //}

    public IEnumerator FadeOutSong(float fadeTime)
    {
        Song currentSong = playlist[_currentTrack];

        while (currentSong.source.volume > 0f)
        {
            currentSong.source.volume -= (.03f / fadeTime) * playlist[_currentTrack].volume;
            yield return new WaitForSecondsRealtime(.03f);
        }

        currentSong.source.Stop();
    }
    #endregion

    public void PlaySong(int index)
    {
        StopAllCoroutines();

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
