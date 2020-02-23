using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Manages bringing up the pause menu
public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject player;
    public EventSystem eventSystem;
    public GameObject selectedObjectOnPause;

    private static bool _gameIsPaused = false;

    private const KeyCode _keyCode1 = KeyCode.Escape;
    private const KeyCode _keyCode2 = KeyCode.P;
    
    void Update()
    {
        if (Input.GetKeyDown(_keyCode1) || Input.GetKeyDown(_keyCode2))
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
}
