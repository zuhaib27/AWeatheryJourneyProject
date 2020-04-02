using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

public class SceneSequenceL01 : MonoBehaviour
{
    public GameObject CameraWaterfall;
    public GameObject CameraSunPuzzle;
    public GameObject CameraWaterwheel;
    public GameObject MainCamera;
    public GameObject PlayerControls;

    public void PlaySequence()
    {
        StartCoroutine(LevelSequence());
    }

    private void Update()
    {
        if (ButtonMappings.GetButtonDown(Button.Jump))
        {
            StopSequence();
        }
    }

    IEnumerator LevelSequence()
    {
        PlayerControls.SetActive(false);
        CameraWaterfall.SetActive(true);
        MainCamera.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        CameraSunPuzzle.SetActive(true);
        CameraWaterfall.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        CameraSunPuzzle.SetActive(false);
        CameraWaterwheel.SetActive(true);
        yield return new WaitForSeconds(4.9f);
        StopSequence();
    }

    private void StopSequence()
    {
        PlayerControls.SetActive(true);
        MainCamera.SetActive(true);
        CameraWaterfall.SetActive(false);
        CameraSunPuzzle.SetActive(false);
        CameraWaterwheel.SetActive(false);
    }
}
