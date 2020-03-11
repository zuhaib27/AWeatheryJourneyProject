using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformOnSun : Sunable
{

    public MoveablePlant moveablePlant;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject g = GameObject.FindGameObjectWithTag("SunablePlant");

        moveablePlant = gameObject.GetComponent<MoveablePlant>();

        // accesses the bool named "isOnFire" and changed it's value.
        
    }

    // Define the action taken when sun ability is applied to plant
    public override void OnSunDown(AbilityEvent e)
    {
        base.OnSunDown(e);
        Debug.Log("sun activated");
        moveablePlant.enabledMovement = !moveablePlant.enabledMovement;


    }
}
