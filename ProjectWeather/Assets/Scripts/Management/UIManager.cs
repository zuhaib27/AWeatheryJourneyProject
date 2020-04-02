using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using AWeatheryJourney;
using UnityEngine.SceneManagement;

// Manages bringing up the pause menu
public class UIManager : MonoBehaviour
{
    public bool startInMenu = true;

    public UISession pauseSession;
    public UISession mainSession;
    public Canvas inGameHUD;
    public EventSystem eventSystem;

    public GameObject player;
    public GameObject playerCamera;
    public GameObject menuCamera;

    public GameObject menuMushrooms;
    public GameObject menuReplacedMushrooms;

    private static bool _gameIsPaused = false;
    private bool _canPause = false;

    #region singleton
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private void Start()
    {
        // To ensure that "OnEnable()" gets called
        pauseSession.gameObject.SetActive(false);
        mainSession.gameObject.SetActive(false);

        if (menuCamera == null)
            menuCamera = playerCamera;

        if (startInMenu && SceneManager.GetActiveScene().buildIndex == (int)LevelIndex.MainMenu)
        {
            LockPlayer();
            menuCamera.SetActive(true);
            playerCamera.SetActive(false);

            mainSession.gameObject.SetActive(true);
            pauseSession.gameObject.SetActive(false);
            inGameHUD.gameObject.SetActive(false);
            _canPause = false;
            menuMushrooms.SetActive(true);
            menuReplacedMushrooms.SetActive(false);
        }
        else
        {
            menuCamera.SetActive(false);
            playerCamera.SetActive(true);

            mainSession.gameObject.SetActive(false);
            pauseSession.gameObject.SetActive(false);
            inGameHUD.gameObject.SetActive(true);
            _canPause = true;
            menuMushrooms.SetActive(false);
            menuReplacedMushrooms.SetActive(true);
        }
    }

    void Update()
    {
        if (_canPause && ButtonMappings.GetButtonDown(AWeatheryJourney.Button.Pause))
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

    public void StartGame()
    {
        StartCoroutine(FadeOutAndStartGame());
    }

    IEnumerator FadeOutAndStartGame()
    {
        CheckpointManager.Instance.LoadBeginningOfLevel();
        yield return new WaitForSecondsRealtime(1f);

        menuCamera.SetActive(false);
        playerCamera.SetActive(true);
        inGameHUD.gameObject.SetActive(true);
        mainSession.gameObject.SetActive(false);
        _canPause = true;
        menuMushrooms.SetActive(false);
        menuReplacedMushrooms.SetActive(true);

        ResumeGame();
    }

    public void ResumeGame()
    {
        UnlockPlayer();
        Time.timeScale = 1f;

        pauseSession.gameObject.SetActive(false);
        _gameIsPaused = false;
    }

    public void PauseGame()
    {
        LockPlayer();
        Time.timeScale = 0f;

        pauseSession.gameObject.SetActive(true);
        _gameIsPaused = true;
    }

    public EventSystem GetEventSystem()
    {
        return eventSystem;
    }

    private void LockPlayer()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.GetComponentInChildren<Assets.Player.Scripts.PlayerInputs>().enabled = false;
        player.GetComponentInChildren<PlayerAbility>().enabled = false;
    }

    private void UnlockPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        player.GetComponentInChildren<Assets.Player.Scripts.PlayerInputs>().enabled = true;
        player.GetComponentInChildren<PlayerAbility>().enabled = true;
    }
}
