using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequenceL01 : MonoBehaviour
{
    public GameObject CameraVertPuzzle;
    public GameObject CameraWaterRise;
    public GameObject CameraWaterfall;
    public GameObject CameraSunPuzzle;
    public GameObject CameraWaterwheel;
    public GameObject MainCamera;
    public GameObject PlayerControls;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelSequence());
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
        yield return new WaitForSeconds(2.9f);

        PlayerControls.SetActive(true);
        MainCamera.SetActive(true);
        CameraWaterwheel.SetActive(false);

    }
}
