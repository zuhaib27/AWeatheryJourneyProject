using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnFreeze : Freezeable
{
    IceGenerator _iceGenerator;
    
    // Start is called before the first frame update
    void Start()
    {
        _iceGenerator = GetComponentInChildren<IceGenerator>();
    }

    // Define the action taken when frost ability is applied to water
    public override void OnFreeze(AbilityEvent e)
    {
        base.OnFreeze(e);

        _iceGenerator.GenerateRadius(e.playerPosition, false);
    }
}
