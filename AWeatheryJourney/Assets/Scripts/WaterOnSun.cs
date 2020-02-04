using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterOnSun : Sunable
{
    new MeshCollider collider;
    new MeshRenderer renderer;

    public Material waterMaterial;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<MeshCollider>();
        renderer = GetComponent<MeshRenderer>();
    }

    // Define the action taken when sun ability is applied to water
    public override void OnSun(int powerLevel)
    {
        base.OnSun(powerLevel);

        collider.isTrigger = true;
        renderer.material = waterMaterial;
    }
}
