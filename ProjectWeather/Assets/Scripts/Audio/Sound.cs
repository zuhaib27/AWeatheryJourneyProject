using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
	public AudioClip[] clips = new AudioClip[1];
    public AudioSource source;

    public float Volume { get { return source.volume; }}

    [Header("Sound Modifiers")]
    [Range(0f, 1f)]
	public float volumeVariance = 0f;
	[Range(0f, 1f)]
	public float pitchVariance = 0f;

    private bool _initialized;
    private float _volume;
    private float _pitch;

    private void Init()
    {
        if (source == null)
        {
            Debug.LogWarning("Sound is missing a source!");
        }

        _volume = source.volume;
        _pitch = source.pitch;
        _initialized = true;

        if (source == null)
        {
            source = new AudioSource();
        }
    }

    public void Play()
    {
        if (!_initialized)
        {
            Init();
        }


        source.Stop();
        source.clip = GetRandomClip();
        source.volume = _volume * (1f + UnityEngine.Random.Range(-volumeVariance, volumeVariance));
        source.pitch = _pitch * (1f + UnityEngine.Random.Range(-pitchVariance, pitchVariance));
        source.Play();
    }

    public void PlayOneShot(float volumeScale = -1)
    {
        if (!_initialized)
        {
            Init();
        }

        if (volumeScale < 0)
            volumeScale = _volume;

        source.Stop();
        volumeScale *= (1f + UnityEngine.Random.Range(-volumeVariance, volumeVariance));
        source.pitch = _pitch * (1f + UnityEngine.Random.Range(-pitchVariance, pitchVariance));
        source.PlayOneShot(GetRandomClip(), volumeScale);
    }

    private AudioClip GetRandomClip()
    {
        return clips[UnityEngine.Random.Range(0, clips.Length)];
    }
}
