using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Footsteps : MonoBehaviour
{
    public Sound audioFloorA;
    public Sound audioFloorB;
    public Sound audioFloorC;
    public Sound audioFloorD;

    public float playSpeed = 2f;

    private AudioSource _source;
    private float _timeLastPlayed;

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
        //_source.spatialBlend = 1f;
        _timeLastPlayed = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        bool playerIsGrounded = true;
        float playerVelocity = 1f;

        if (playerIsGrounded && Time.time - _timeLastPlayed > playerVelocity / playSpeed)
        {
            Debug.Log("Playing step");
            Sound s = GetCurrentStepSound();
            _source.clip = s.clip;
            _source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            _source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
            _source.Play();
            _timeLastPlayed = Time.time;
        }
    }

    private Sound GetCurrentStepSound()
    {
        return audioFloorA;
    }
}
