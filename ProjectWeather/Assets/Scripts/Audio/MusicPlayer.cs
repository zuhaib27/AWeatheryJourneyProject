using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    [Range(0f, 5f)]
    public double delayBetweenSongs = 1;
    [Range(0f, 5f)]
    public float fadeTimeOnSongEnd = 1f;

    public Song[] playlist;

    private int _currentTrack = -1;
    private AudioSource _source;

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_source.isPlaying)
        {
            StopAllCoroutines();

            _currentTrack = (_currentTrack + 1) % playlist.Length;
            Song nextSong = playlist[_currentTrack];
            _source.clip = nextSong.clip;
            _source.volume = nextSong.volume;
            _source.Play((ulong)(_source.clip.frequency * delayBetweenSongs));

            StartCoroutine(WaitForFadeOut(_source.clip.length - fadeTimeOnSongEnd));
        }

        //if (_source.clip.length - _source.time < fadeTimeOnSongEnd)
        //{
        //    _source.volume -= (Time.deltaTime / fadeTimeOnSongEnd) * playlist[_currentTrack].volume;
        //}
    }

    IEnumerator WaitForFadeOut(float waitSeconds)
    {
        yield return new WaitForSecondsRealtime(waitSeconds);
        StartCoroutine(FadeOutSong(fadeTimeOnSongEnd));
    }

    public IEnumerator FadeOutSong(float fadeTime)
    {
        while (_source.isPlaying)
        {
            _source.volume -= (.03f / fadeTime) * playlist[_currentTrack].volume;
            yield return new WaitForSecondsRealtime(.03f);
        }
    }

    public string GetCurrentTrack()
    {
        return playlist[_currentTrack].name;
    }
}
