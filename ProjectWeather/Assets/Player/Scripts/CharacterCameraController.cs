using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CharacterCameraController : MonoBehaviour
{
    //public CinemachineStateDrivenCamera StateDrivenCam;
    public CinemachineFreeLook FreeLookCamWorld;
    public float SecondsAfterLeavingPlatforms = 10f;

    private bool _onTrigger = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            //Debug.Log("Player hit trigger");
            _onTrigger = true;

            FreeLookCamWorld.m_Priority = 20;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            //Debug.Log("Player exit trigger");
            _onTrigger = false;
            StartCoroutine(DelayBeforeExitEnumerator(other));
        }
    }

    private IEnumerator DelayBeforeExitEnumerator(Collider other)
    {
        yield return new WaitForSeconds(SecondsAfterLeavingPlatforms);

        // Check if they are still off the trigger after the delay
        if (!_onTrigger)
        {
            FreeLookCamWorld.m_Priority = 0;
        }
    }
}
