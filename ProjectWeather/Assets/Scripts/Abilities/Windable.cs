using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windable : Interactible
{
  // Called when wind ability is activated on/near this object
  public virtual void OnWindDown(AbilityEvent e)
  {
    // This function is meant to be overwritten
  }

    // Called when wind ability is activated on/near this object
    public virtual void OnWind(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when wind ability is activated on/near this object
    public virtual void OnWindUp(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }
}
