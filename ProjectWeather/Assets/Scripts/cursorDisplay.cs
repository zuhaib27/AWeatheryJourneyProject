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
        if (Input.GetButtonDown("Cancel"))
        {
            Cursor.visible = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.visible = false;
        }
    }
}
