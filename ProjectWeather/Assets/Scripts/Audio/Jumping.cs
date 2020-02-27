using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Jumping : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public Sound jumpSound;
    public Sound landSound;

    private Assets.Player.Scripts.MyCharacterController _characterController;

    private AudioSource _source;

    private bool _playerIsOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.spatialBlend = 1f;
        _source.outputAudioMixerGroup = mixerGroup;

        _characterController = GetComponentInChildren<Assets.Player.Scripts.MyCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.IsPlayerOnGround())
        {
            if (!_playerIsOnGround)
            {
                _source.Stop();
                _source.clip = landSound.clip;
                _source.volume = landSound.volume * (1f + UnityEngine.Random.Range(-landSound.volumeVariance, landSound.volumeVariance));
                _source.pitch = landSound.pitch * (1f + UnityEngine.Random.Range(-landSound.pitchVariance, landSound.pitchVariance));
                _source.Play();
                _playerIsOnGround = true;
            }
        }
        else
        {
            if (_playerIsOnGround)
            {
                _source.Stop();
                _source.clip = jumpSound.clip;
                _source.volume = jumpSound.volume * (1f + UnityEngine.Random.Range(-jumpSound.volumeVariance, jumpSound.volumeVariance));
                _source.pitch = jumpSound.pitch * (1f + UnityEngine.Random.Range(-jumpSound.pitchVariance, jumpSound.pitchVariance));
                _source.Play();
                _playerIsOnGround = false;
            }
        }
    }
}
