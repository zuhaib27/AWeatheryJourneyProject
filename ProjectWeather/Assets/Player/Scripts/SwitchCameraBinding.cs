using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCameraBinding : MonoBehaviour
{
    private CinemachineFreeLook _freeLookCam;
    private bool _onTrigger = false;
    private float _secondsDelay = 4f;


    private void Awake()
    {
        _freeLookCam = FindObjectOfType<CinemachineFreeLook>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            //Debug.Log("Player hit trigger");
            _onTrigger = true;

            _freeLookCam.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
            _freeLookCam.m_Heading.m_Definition = CinemachineOrbitalTransposer.Heading.HeadingDefinition.TargetForward;
            _freeLookCam.m_Heading.m_Bias = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            Debug.Log("Player exit trigger");
            _onTrigger = false;
            StartCoroutine(DelayBeforeExitEnumerator(other));
        }
    }

    private IEnumerator DelayBeforeExitEnumerator(Collider other)
    {
        yield return new WaitForSeconds(_secondsDelay);

        // Check if they are still off the trigger after delay
        if (!_onTrigger)
        {
            _freeLookCam.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
        }
    }
}
