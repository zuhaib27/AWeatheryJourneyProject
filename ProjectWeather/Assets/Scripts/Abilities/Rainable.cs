using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainable : MonoBehaviour
{
    // Called when rain ability is activated on this object
    public virtual void OnRainDown(int powerLevel)
    {
    // This function is meant to be overwritten
    }

    // Called when rain ability is activated on this object
    public virtual void OnRain(int powerLevel)
    {
        // This function is meant to be overwritten
    }

    // Called when rain ability is activated on this object
    public virtual void OnRainUp(int powerLevel)
    {
        // This function is meant to be overwritten
    }
}