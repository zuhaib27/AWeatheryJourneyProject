using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterOnFreeze : Freezeable
{
    new MeshCollider collider;
    new MeshRenderer renderer;
    
    public Material iceMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        renderer = GetComponent<MeshRenderer>();
    }

    // Define the action taken when frost ability is applied to water
    public override void OnFreeze(int powerLevel)
    {
        base.OnFreeze(powerLevel);

        collider.isTrigger = false;
        renderer.material = iceMaterial;
    }
}
