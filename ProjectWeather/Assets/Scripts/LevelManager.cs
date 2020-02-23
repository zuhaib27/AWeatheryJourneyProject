using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Use this to load levels
public class LevelManager : MonoBehaviour
{
    public Animator animator;

    private int _levelToLoad;

    public static LevelManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void LoadLevel(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
        _levelToLoad = levelIndex;
    }

    public void LoadLevel(LevelIndex levelIndex)
    {
        LoadLevel((int)levelIndex);
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
    MainMenu,
    Level1,
    Level2,
    Level3
}
