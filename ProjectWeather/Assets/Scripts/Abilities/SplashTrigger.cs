using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSounds playerSounds = other.GetComponentInChildren<PlayerSounds>();
            playerSounds.PlaySplash();
        }
    }
}
