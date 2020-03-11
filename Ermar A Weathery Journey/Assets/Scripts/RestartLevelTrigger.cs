using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager.Instance.ReloadLevel();
        }
    }
}
