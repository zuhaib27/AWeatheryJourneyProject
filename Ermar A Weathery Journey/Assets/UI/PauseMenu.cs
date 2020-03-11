using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenu : Menu
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

    override public void GoBack()
    {
        UIManager.Instance.ResumeGame();
    }
}
