using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainable : Interactible
{
    // Called when rain ability is activated on this object
    public virtual void OnRainDown(AbilityEvent e)
    {
    // This function is meant to be overwritten
    }

    // Called when rain ability is activated on this object
    public virtual void OnRain(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when rain ability is activated on this object
    public virtual void OnRainUp(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }
}
