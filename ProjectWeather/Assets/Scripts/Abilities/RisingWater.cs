using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : Rainable
{
    public float minY;
    public float maxY;

    public float floodSpeed = 1f;

    public override void OnRain(int powerLevel)
    {
        base.OnRain(powerLevel);

        if (transform.position.y < maxY)
        {
            transform.Translate(transform.up * floodSpeed * Time.deltaTime);
        }
    }
}
