using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelTrigger : MonoBehaviour
{
    public Transform player;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Restart Level");
        if (other.gameObject == player)
        {
            Debug.Log("Restarting Level");
            LevelManager.Instance.ReloadLevel();
        }
    }
}
