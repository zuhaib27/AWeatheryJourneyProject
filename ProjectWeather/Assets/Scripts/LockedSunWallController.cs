using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedSunWallController : MonoBehaviour
{
    public GameObject Door;

    public GameObject CameraDoor;
    public GameObject MainCamera;

    public static LockedSunWallController instance;
    public bool sunKey1 = false;
    public bool sunKey2 = false;
    public bool sunKey3 = false;
    public bool sunKey4 = false;

    private bool _doorOpened = false;
    private float _originalypos;
    private bool _startAnimation = true;

    private AudioSource _audioDoor;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _originalypos = Door.transform.position.y;
        _audioDoor = GetComponentInChildren<AudioSource>();
        CameraDoor.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //play with different bools to set combinations.
        if(sunKey1 && sunKey2 && sunKey3 && sunKey4 && !_doorOpened)
        {
            //shake camera, maybe play sound effect?
            if (_startAnimation)
            {
                Debug.Log("shake");
                StartCoroutine(OpenDoorSequence());
                _startAnimation = false; 
            }
            Door.transform.Translate(Vector3.up * Time.deltaTime * 10 * .5f);
        }
        if (Door.transform.position.y > (_originalypos + 10))
        {
            _doorOpened = true;
        }
        
    }

    IEnumerator OpenDoorSequence()
    {
        CameraDoor.SetActive(true);
        MainCamera.SetActive(false);
        _audioDoor.Play(0);
        yield return new WaitForSeconds(2.5f);
        MainCamera.SetActive(true);
        CameraDoor.SetActive(false);
        
    }
}
