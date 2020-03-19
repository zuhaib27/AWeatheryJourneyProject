using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimOnSun : Interactible
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
        transform.GetComponent<Assets.Platforms.Scripts.MyMovablePlantAnim>().enabled = true;
        transform.GetComponent<KinematicCharacterController.PhysicsMover>().enabled = true;
        transform.GetComponent<Assets.Platforms.Scripts.MyMovablePlantAnim>().timer = 0;


    }
}
