using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : Menu
{
    public void LoadTutorial()
    {
        LevelManager.Instance.LoadLevel(LevelIndex.TutorialLevel);
    }

    public void LoadMainLevel()
    {
        UIManager.Instance.StartGame();
    }
}
