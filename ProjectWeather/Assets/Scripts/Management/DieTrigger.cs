using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Player.Scripts;

public class DieTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player has died!");
            // Disable any further inputs from the Player
            GameObject.Find("PlayerInputs").GetComponent<PlayerInputs>().enabled = false;

            // Make the Character stop moving
            var characterInputs = new PlayerCharacterInputs
            {
                // Build the CharacterInputs struct
                MoveAxisForward = 0f,
                MoveAxisRight = 0f,
                JumpDown = false,
            };

            // Apply inputs to character
            other.GetComponent<MyCharacterController>().SetInputs(ref characterInputs);

            // Disable the Player's abilities
            other.GetComponent<PlayerAbility>().enabled = false;
            CheckpointManager.Instance.LoadCheckpoint();
        }
    }
}
