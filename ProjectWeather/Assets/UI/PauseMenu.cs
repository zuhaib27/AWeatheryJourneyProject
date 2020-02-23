using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.LoadLevel(LevelIndex.MainMenu);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.ReloadLevel();
    }
}
