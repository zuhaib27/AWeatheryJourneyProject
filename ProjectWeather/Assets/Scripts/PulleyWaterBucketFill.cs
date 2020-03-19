using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyWaterBucketFill : Interactible
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void OnRain(AbilityEvent e)
    {
        this.transform.parent.GetComponent<PulleySystem>().enableRain = true;
    }

    public override void OnRainUp(AbilityEvent e)
    {
        this.transform.parent.GetComponent<PulleySystem>().enableRain = false;
    }
}
