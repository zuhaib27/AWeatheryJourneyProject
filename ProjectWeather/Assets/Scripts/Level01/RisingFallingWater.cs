using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingFallingWater : MonoBehaviour
{
    private enum State { Rising, Falling, WaitingForRise, WaitingForFall };

    public float floodSpeed = 1f;
    public float waitSecondsBeforeSwitchingDirection = 2f;

    public Transform maxWaterLevel;
    public Transform minWaterLevel = null;
    
    private State _state = State.Rising;
    private float _curWaitTime = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(_state)
        {
            case State.Rising:
                Rise();
                break;
            case State.Falling:
                Fall();
                break;
            case State.WaitingForRise:
                _curWaitTime += Time.deltaTime;
                if (_curWaitTime > waitSecondsBeforeSwitchingDirection)
                {
                    Debug.Log("Gonig to state 'Rising'");
                    _state = State.Rising;
                    _curWaitTime = 0f;
                }
                break;
            case State.WaitingForFall:
                _curWaitTime += Time.deltaTime;
                if (_curWaitTime > waitSecondsBeforeSwitchingDirection)
                {
                    Debug.Log("Gonig to state 'Falling'");
                    _state = State.Falling;
                    _curWaitTime = 0f;
                }
                break;
        }
    }


    public void Rise()
    {
        float currentY = transform.TransformPoint(0, 0, 0).y;

        if (currentY < maxWaterLevel.position.y)
        {
            float yTranslate = Mathf.Min(floodSpeed * Time.deltaTime, maxWaterLevel.position.y - currentY);
            transform.Translate(transform.up * yTranslate);
        }
        else
        {
            Debug.Log("Gonig to state 'WaitingForFall'");
            _state = State.WaitingForFall;
        }
    }

    public void Fall()
    {
        float currentY = transform.TransformPoint(0, 0, 0).y;

        if (currentY > minWaterLevel.position.y)
        {
            float yTranslate = Mathf.Min(floodSpeed * Time.deltaTime, currentY - minWaterLevel.position.y);
            transform.Translate(-transform.up * yTranslate);
        }
        else
        {
            Debug.Log("Gonig to state 'WaitingForRise'");
            _state = State.WaitingForRise;
        }
    }
}
