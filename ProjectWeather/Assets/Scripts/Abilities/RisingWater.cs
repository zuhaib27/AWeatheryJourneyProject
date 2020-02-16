using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWater : Rainable
{
    public float floodSpeed = 1f;

    float _maxWaterLevel;
    Transform _iceLevel;
    IceGenerator _iceGenerator;

    private void Start()
    {
        _maxWaterLevel = transform.parent.GetChild(1).position.y;

        _iceLevel = transform.GetChild(0);
        _iceGenerator = _iceLevel.gameObject.GetComponent<IceGenerator>();
    }

    public override void OnRainDown(AbilityEvent e)
    {
        base.OnRainDown(e);

        // Remove ice
        _iceGenerator.ResetGrid();
    }

    public override void OnRain(AbilityEvent e)
    {
        base.OnRain(e);

        if (transform.position.y + transform.localScale.y / 2f < _maxWaterLevel)
        {
            Vector3 newScale = transform.localScale;
            newScale.y += floodSpeed * Time.deltaTime;
            transform.localScale = newScale;

            transform.Translate(transform.up * floodSpeed / 2f * Time.deltaTime);

            // Move ice level
            //_iceLevel.Translate(transform.up * floodSpeed * Time.deltaTime);
        }
    }

    public override void OnRainUp(AbilityEvent e)
    {
        base.OnRainUp(e);

        // Recalculate ice grid
        _iceGenerator.ResetGrid();
    }
}
