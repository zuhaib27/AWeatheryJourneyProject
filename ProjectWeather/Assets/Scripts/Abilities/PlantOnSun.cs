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
  public override void OnSunDown(int powerLevel)
  {
    base.OnSunDown(powerLevel);

    if (!this.anim.GetCurrentAnimatorStateInfo(0).IsName("growPlant"))
    {
      anim.Play("growPlant", -1, 0f);
    }
    
    
  }
}
