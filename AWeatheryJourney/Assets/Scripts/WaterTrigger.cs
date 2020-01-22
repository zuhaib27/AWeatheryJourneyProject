using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject target;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
            gameManager.ResetLevel();
    }
}
