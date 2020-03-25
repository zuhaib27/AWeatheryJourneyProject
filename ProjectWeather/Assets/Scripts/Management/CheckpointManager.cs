using System.Collections;
using System.Collections.Generic;
using Assets.Player.Scripts;
using UnityEngine;
using System;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public event Action OnSpawn;

    // Private variables
    private Vector3 _currentCheckpoint;
    private Animator _animator;
    private MyCharacterController _cc;
    private PlayerInputs _playerInputs;
    private PlayerAbility _playerAbility;

    private void Awake()
    {
        #region singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }
        #endregion

        _animator = GetComponent<Animator>();
        _cc = FindObjectOfType<MyCharacterController>();
        _playerInputs = FindObjectOfType<PlayerInputs>();
        _playerAbility = FindObjectOfType<PlayerAbility>();
    }

    private void Start()
    {
        // Initialize Checkpoint0 to the level's spawn point
        _currentCheckpoint = _cc.GetCharacterPosition();
        _currentCheckpoint.y += 0.5f;
    }

    public void SetCheckpoint(Vector3 pos)
    {
        _currentCheckpoint = pos;
        _currentCheckpoint.y += 0.5f;
    }

    public void LoadCheckpoint()
    {
        _animator.ResetTrigger("FadeOut");
        _animator.ResetTrigger("FadeIn");
        _animator.SetTrigger("FadeOut");
        MusicPlayer.Instance.FadeOutSong(1);
    }

    // Called by animator when death fade out is completed
    public void OnFadeOutComplete()
    {
        _animator.SetTrigger("FadeIn");
        _playerInputs.enabled = true;
        _playerAbility.enabled = true;
        _cc.Motor.SetPosition(_currentCheckpoint);

        OnSpawn.Invoke();
    }
}
