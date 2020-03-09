using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedSunWallController : MonoBehaviour
{
    public GameObject Door;
    public static LockedSunWallController instance;
    public bool sunKey1 = false;
    public bool sunKey2 = false;
    public bool sunKey3 = false;
    public bool sunKey4 = false;
    private bool doorOpened = false;
    private float originalypos;
    private bool startAnimation = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        originalypos = Door.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //play with different bools to set combinations.
        if(sunKey1 && sunKey2 && sunKey3 && sunKey4 && !doorOpened)
        {
            //shake camera, maybe play sound effect?
            if (startAnimation)
            {
                Debug.Log("shake");
                startAnimation = false; 
            }
            Door.transform.Translate(Vector3.up * Time.deltaTime * 10);
        }
        if (Door.transform.position.y > (originalypos + 10))
        {
            doorOpened = true;
        }
        
    }
}
