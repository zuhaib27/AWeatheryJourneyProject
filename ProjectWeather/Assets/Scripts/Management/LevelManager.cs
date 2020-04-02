using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Use this to load levels
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public Animator animator;

    private int _levelToLoad;

    private void Awake()
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

    private void LoadLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        _levelToLoad = levelIndex;
        MusicPlayer.Instance.FadeOutSong(1);
    }

    public void LoadLevel(LevelIndex levelIndex)
    {
        if (levelIndex == LevelIndex.NextLevel)
        {
            LoadNextLevel();
        }
        else
        {
            LoadLevel((int)levelIndex);
        }
    }

    public void LoadNextLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(_levelToLoad);
    }
}

// Valid levels to load
public enum LevelIndex
{
    TutorialLevel,
    Level1,
    NextLevel,
    MainMenu = Level1
}
