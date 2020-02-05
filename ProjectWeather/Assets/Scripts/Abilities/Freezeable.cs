using UnityEngine;

public class Freezeable : MonoBehaviour
{
    // Called when frost ability is activated on this object
    public virtual void OnFreeze(int powerLevel)
    {
        // This function is meant to be overwritten
        Debug.Log("Activated frost on " + gameObject.name);
    }
}
