using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorDisplay : MonoBehaviour
{
    // initialization
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.visible = false;
        }
    }
}
