using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSequence : MonoBehaviour
{
    public GameObject CameraLevel;
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
        CameraLevel.SetActive(true);
        MainCamera.SetActive(false);
        yield return new WaitForSeconds(6f);
        PlayerControls.SetActive(true);
        MainCamera.SetActive(true);
        CameraLevel.SetActive(false);

    }
}

