using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RisingWater : Interactible
{
    public float floodSpeed = 1f;

    public Transform maxWaterLevel;

    public Ice iceObject;

    public event Action OnReachMaxLevel;

    private bool _alreadyCalled = false;
    private bool _waterLevelChanged = false;
    private bool _isRaining = false;

    public AudioSource fillSound;

    public void LateUpdate()
    {
        if (!_isRaining)
        {
            if (fillSound)
                fillSound.Stop();
        }

        _isRaining = false;
    }

    // Increase water level
    public override void OnRain(AbilityEvent e)
    {
        base.OnRain(e);
        _isRaining = true;

        if (fillSound && !fillSound.isPlaying)
            fillSound.Play();

        float currentY = transform.TransformPoint(0, 0, 0).y;

        if (currentY < maxWaterLevel.position.y)
        {
            if (iceObject)
                iceObject.ResetGrid();

            //Vector3 newScale = transform.localScale;
            //newScale.y += floodSpeed * Time.deltaTime;
            //transform.localScale = newScale;
            
            float yTranslate = Mathf.Min(floodSpeed * Time.deltaTime, maxWaterLevel.position.y - currentY);
            transform.Translate(transform.up * yTranslate);
            _waterLevelChanged = true;
        }
        else if (!_alreadyCalled)
        {
            OnReachMaxLevel.Invoke();
            _alreadyCalled = true;
        }
    }

    public bool DidWaterLevelChange()
    {
        bool temp = _waterLevelChanged;
        _waterLevelChanged = false;
        return temp;
    }

    //public float GetLevelOfWater()
    //{
    //    float currentY = transform.TransformPoint(0, 0, 0).y;
    //    return 
    //}
}
