using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFreeze : MonoBehaviour
{
    private bool flag = true;
    public GameObject iceObj;


    void Start()
    {
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        { 
            if (flag)
            {
                Debug.Log("Freeze Enable!");
                iceObj.active = false;
                flag = false;
            }
            else if (!flag)
            {
                Debug.Log("Freeze Disable!");
                iceObj.active = true;
                flag = true;
            }

        }
    }
}
