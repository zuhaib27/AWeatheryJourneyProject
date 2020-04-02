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

    public void LoadLastCheckpoint()
    {
        Time.timeScale = 1f;
        //LevelManager.Instance.ReloadLevel();
        CheckpointManager.Instance.LoadCheckpoint();
        UIManager.Instance.ResumeGame();
    }

    override public void GoBack()
    {
        UIManager.Instance.ResumeGame();
    }
}
