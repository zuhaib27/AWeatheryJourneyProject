using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

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

        CameraLevel.enabled = true;
        MainCamera.enabled = false;
        yield return new WaitForSeconds(6f);

        StopSequence();

    }

    private void StopSequence()
    {
        StopAllCoroutines();
        CameraLevel.enabled = false;
        MainCamera.enabled = true;
        PlayerControls.SetActive(true);
    }
}

