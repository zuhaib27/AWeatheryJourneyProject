using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunable : Interactible
{
    // Called when sun ability is activated on this object
    public virtual void OnSunDown(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when sun ability is activated on this object
    public virtual void OnSun(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when sun ability is activated on this object
    public virtual void OnSunUp(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }
}
