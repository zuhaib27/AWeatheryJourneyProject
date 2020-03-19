using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleyWaterBucketFill : Interactible
{
    public GameObject water;
    public float verticalSpeed = 0.5f;
    public Transform maxWaterLevel;
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
        if(water.transform.position.y < maxWaterLevel.position.y)
        {
            water.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        }
        
    }

    public override void OnRainUp(AbilityEvent e)
    {
        this.transform.parent.GetComponent<PulleySystem>().enableRain = false;
    }
}
