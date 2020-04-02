using System.Collections;
using System.Collections.Generic;
using Assets.Player.Scripts;
using UnityEngine;
using System;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }

    public event Action OnSpawn;

    public Transform startPoint;
    public Transform tempStartPoint;

    // Private variables
    private Transform _currentCheckpoint;
    private Animator _animator;
    private MyCharacterController _cc;
    private PlayerInputs _playerInputs;
    private PlayerAbility _playerAbility;

    private bool _isBeginningOfLevel = false;

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
        if (tempStartPoint)
            _currentCheckpoint = tempStartPoint;
        else
            _currentCheckpoint = startPoint;

        SpawnPlayer();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        _currentCheckpoint = checkpoint;
    }

    public void LoadBeginningOfLevel()
    {
        if (tempStartPoint)
            _currentCheckpoint = tempStartPoint;
        else
            _currentCheckpoint = startPoint;

        _isBeginningOfLevel = true;
        LoadCheckpoint();
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

        SpawnPlayer();

        if (_isBeginningOfLevel)
        {
            FindObjectOfType<SceneSequenceL01>().PlaySequence();
            _isBeginningOfLevel = false;
        }
    }

    private void SpawnPlayer()
    {
        _cc.Motor.SetPositionAndRotation(_currentCheckpoint.position, _currentCheckpoint.rotation);

        if (OnSpawn != null)
            OnSpawn.Invoke();
    }
}
