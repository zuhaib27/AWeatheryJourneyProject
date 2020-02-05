using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class PlantOnSun : Sunable
{

  new MeshCollider collider;
  new MeshRenderer renderer;
  public Animator anim;

  // Start is called before the first frame update
  void Start()
  {
    collider = GetComponent<MeshCollider>();
    renderer = GetComponent<MeshRenderer>();
    anim = GetComponent<Animator>();
  }

  // Define the action taken when sun ability is applied to plant
  public override void OnSun(int powerLevel)
  {
    base.OnSun(powerLevel);
    anim.Play("growPlant", -1, 0f);
  }
}
