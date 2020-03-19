using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutActivatorOnSun : Interactible
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnSunDown(AbilityEvent e)
    {
        transform.parent.GetComponent<Assets.Platforms.Scripts.SunElevatorMovingPlatform>().enabled = true;
        transform.parent.GetComponent<KinematicCharacterController.PhysicsMover>().enabled = true;
        GetComponentInChildren<Light>().enabled = true;
    }
}
