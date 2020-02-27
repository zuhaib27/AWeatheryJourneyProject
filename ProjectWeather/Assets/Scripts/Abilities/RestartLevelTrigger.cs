using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Restart Level");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Restarting Level");
            LevelManager.Instance.ReloadLevel();
        }
    }
}
