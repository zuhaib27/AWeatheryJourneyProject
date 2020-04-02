using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

public class SceneSequenceL01 : MonoBehaviour
{
    public GameObject CameraVertPuzzle;
    public GameObject CameraWaterRise;
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

        CameraVertPuzzle.SetActive(true);
        MainCamera.SetActive(false);
        yield return new WaitForSeconds(1.9f);

        CameraWaterRise.SetActive(true);
        CameraVertPuzzle.SetActive(false);
        yield return new WaitForSeconds(1.9f);

        CameraWaterfall.SetActive(true);
        CameraWaterRise.SetActive(false);
        yield return new WaitForSeconds(4f);

        CameraSunPuzzle.SetActive(true);
        CameraWaterfall.SetActive(false);
        yield return new WaitForSeconds(2f);

        CameraSunPuzzle.SetActive(false);
        CameraWaterwheel.SetActive(true);
        yield return new WaitForSeconds(4.9f);
        StopSequence();
    }

    private void StopSequence()
    {
        StopAllCoroutines();
        PlayerControls.SetActive(true);
        MainCamera.SetActive(true);
        CameraVertPuzzle.SetActive(false);
        CameraWaterRise.SetActive(false);
        CameraWaterfall.SetActive(false);
        CameraSunPuzzle.SetActive(false);
        CameraWaterwheel.SetActive(false);
    }
}
