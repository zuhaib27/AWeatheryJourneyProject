using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelTrigger : MonoBehaviour
{
    public LevelIndex nextLevel = LevelIndex.NextLevel;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.LoadLevel(nextLevel);
        }
    }
}
