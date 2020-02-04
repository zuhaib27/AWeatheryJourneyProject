using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunable : MonoBehaviour
{
    // Called when sun ability is activated on this object
    public virtual void OnSun(int powerLevel)
    {
        // This function is meant to be overwritten
        Debug.Log("Activated sun on " + gameObject.name);
    }
}
