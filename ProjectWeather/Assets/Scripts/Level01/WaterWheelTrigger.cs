using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWheelTrigger : Interactible
{
    public WaterWheel waterWheel;

    public override void OnRain(AbilityEvent e)
    {
        waterWheel.OnRain();
    }
}
