using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windable : MonoBehaviour
{
  // Called when wind ability is activated on/near this object
  public virtual void OnWind(int powerLevel)
  {
    // This function is meant to be overwritten
    Debug.Log("... effected " + gameObject.name);
  }
}
