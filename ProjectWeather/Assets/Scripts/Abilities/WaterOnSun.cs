using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterOnSun : Sunable
{
    public Material waterMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Define the action taken when sun ability is applied to water
    public override void OnSunDown(int powerLevel)
    {
        base.OnSunDown(powerLevel);

        //collider.isTrigger = true;
        //renderer.material = waterMaterial;
    }
}
