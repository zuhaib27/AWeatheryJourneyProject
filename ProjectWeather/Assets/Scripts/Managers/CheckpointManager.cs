using System.Collections;
using System.Collections.Generic;
using Assets.Player.Scripts;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance { get; private set; }


    // Private variables
    private Vector3 _currentCheckpoint;
    private Animator _animator;
    private MyCharacterController _cc;

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
        StartCoroutine(FindObjectOfType<MusicPlayer>().FadeOutSong(1));
    }

    public void OnFadeOutComplete()
    {
        _animator.SetTrigger("FadeIn");
        _cc.Motor.SetPosition(_currentCheckpoint);
    }
}
