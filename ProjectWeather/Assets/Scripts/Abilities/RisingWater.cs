using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : Rainable
{
    public float floodSpeed = 1f;

    public Transform maxWaterLevel;
    
    Transform _iceLevel;
    IceGenerator _iceGenerator;

    // Start
    private void Start()
    {
        _iceLevel = transform.GetChild(0);
        _iceGenerator = _iceLevel.gameObject.GetComponent<IceGenerator>();
    }

    // Remove ice
    public override void OnRainDown(AbilityEvent e)
    {
        base.OnRainDown(e);
    }

    // Increase water level
    public override void OnRain(AbilityEvent e)
    {
        base.OnRain(e);

        float currentY = transform.TransformPoint(0, .5f, 0).y;

        if (currentY < maxWaterLevel.position.y)
        {
            _iceGenerator.ResetGrid();
            Vector3 newScale = transform.localScale;
            newScale.y += floodSpeed * Time.deltaTime;
            transform.localScale = newScale;
            
            float yTranslate = Mathf.Min(floodSpeed / 2f * Time.deltaTime, maxWaterLevel.position.y - currentY);
            transform.Translate(transform.up * yTranslate);
        }
    }

    // Reset ice grid
    public override void OnRainUp(AbilityEvent e)
    {
        base.OnRainUp(e);
    }
}
