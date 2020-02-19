using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnSun : Sunable
{
    IceGenerator _iceGenerator;

    // Start is called before the first frame update
    void Start()
    {
        _iceGenerator = GetComponentInChildren<IceGenerator>();
    }

    // Define the action taken when sun ability is applied to water
    public override void OnSun(AbilityEvent e)
    {
        base.OnSunDown(e);

        _iceGenerator.GenerateRadius(e.playerPosition, true);
    }
}
