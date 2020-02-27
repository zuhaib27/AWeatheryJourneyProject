using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Manages bringing up the pause menu
public class UIManager : MonoBehaviour
{
    public bool canPause = true;

    public GameObject pauseMenu;
    public GameObject player;
    public EventSystem eventSystem;
    public GameObject selectedObjectOnPause;

    private static bool _gameIsPaused = false;

    private const string _pauseInput = "Pause";

    #region singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    void Update()
    {
        if (canPause && Input.GetButtonDown(_pauseInput))
        {
            if (_gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }

        }
    }

    public bool GameIsPaused()
    {
        return _gameIsPaused;
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<Assets.Player.Scripts.MyPlayer>().enabled = true;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        _gameIsPaused = false;
    }

    public void PauseGame()
    {
        eventSystem.SetSelectedGameObject(selectedObjectOnPause);
        Cursor.lockState = CursorLockMode.None;
        player.GetComponent<Assets.Player.Scripts.MyPlayer>().enabled = false;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        _gameIsPaused = true;
    }

    public EventSystem GetEventSystem()
    {
        return eventSystem;
    }
}
