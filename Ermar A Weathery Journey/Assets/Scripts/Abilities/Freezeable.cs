using UnityEngine;

public class Freezeable : MonoBehaviour
{
    // Called when frost ability is activated on this object
    public virtual void OnFreezeDown(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when frost ability is activated on this object
    public virtual void OnFreeze(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }

    // Called when frost ability is activated on this object
    public virtual void OnFreezeUp(AbilityEvent e)
    {
        // This function is meant to be overwritten
    }
}
