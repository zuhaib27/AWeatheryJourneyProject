using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public Camera CameraLevel;
    public Camera MainCamera;
    public GameObject PlayerControls;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LevelSequence());
    }

    IEnumerator LevelSequence()
    {
        PlayerControls.SetActive(false);

        CameraLevel.enabled = true;
        MainCamera.enabled = false;
        yield return new WaitForSeconds(6f);
        PlayerControls.SetActive(true);
        MainCamera.enabled = true;
        CameraLevel.enabled = false;

    }
}

