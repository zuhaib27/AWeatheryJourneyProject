using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunKeyOnSun4 : Interactible
{
    public Light keyLight;
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
        base.OnSun(e);
        Debug.Log("activated key1");
        keyLight.enabled = true;
        LockedSunWallController.instance.sunKey4 = true;
    }
}
