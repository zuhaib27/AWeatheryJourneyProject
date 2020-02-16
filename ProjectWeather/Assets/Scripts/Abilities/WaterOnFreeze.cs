using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterOnFreeze : Freezeable
{
    IceGenerator iceGenerator;

    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        iceGenerator = GetComponentInChildren<IceGenerator>();
    }

    // Define the action taken when frost ability is applied to water
    public override void OnFreeze(int powerLevel)
    {
        base.OnFreeze(powerLevel);

        iceGenerator.GenerateRadius(player.position);
    }
}
