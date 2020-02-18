using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : Rainable
{
    public float floodSpeed = 1f;

    float _maxWaterLevel;
    Transform _iceLevel;
    IceGenerator _iceGenerator;

    // Start
    private void Start()
    {
        _maxWaterLevel = transform.parent.GetChild(1).position.y;

        _iceLevel = transform.GetChild(0);
        _iceGenerator = _iceLevel.gameObject.GetComponent<IceGenerator>();
    }

    // Remove ice
    public override void OnRainDown(AbilityEvent e)
    {
        base.OnRainDown(e);
        
        _iceGenerator.ResetGrid();
    }

    // Increase water level
    public override void OnRain(AbilityEvent e)
    {
        base.OnRain(e);

        if (transform.position.y + transform.localScale.y / 2f < _maxWaterLevel)
        {
            Vector3 newScale = transform.localScale;
            newScale.y += floodSpeed * Time.deltaTime;
            transform.localScale = newScale;

            transform.Translate(transform.up * floodSpeed / 2f * Time.deltaTime);
        }
    }

    // Reset ice grid
    public override void OnRainUp(AbilityEvent e)
    {
        base.OnRainUp(e);
        
        _iceGenerator.ResetGrid();
    }
}
