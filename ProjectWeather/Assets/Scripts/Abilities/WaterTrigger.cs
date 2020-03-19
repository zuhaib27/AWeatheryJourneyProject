using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSounds playerSounds = other.GetComponent<PlayerSounds>();
            playerSounds.PlaySplash();
        }
    }
}
