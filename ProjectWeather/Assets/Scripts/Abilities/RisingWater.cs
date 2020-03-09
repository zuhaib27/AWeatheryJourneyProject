using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : Interactible
{
    public float floodSpeed = 1f;

    public Transform maxWaterLevel;

    public Ice iceObject;

    // Increase water level
    public override void OnRain(AbilityEvent e)
    {
        base.OnRain(e);

        float currentY = transform.TransformPoint(0, .5f, 0).y;

        if (currentY < maxWaterLevel.position.y)
        {
            if (iceObject)
                iceObject.ResetGrid();

            Vector3 newScale = transform.localScale;
            newScale.y += floodSpeed * Time.deltaTime;
            transform.localScale = newScale;
            
            float yTranslate = Mathf.Min(floodSpeed / 2f * Time.deltaTime, maxWaterLevel.position.y - currentY);
            transform.Translate(transform.up * yTranslate);
        }
    }
}
