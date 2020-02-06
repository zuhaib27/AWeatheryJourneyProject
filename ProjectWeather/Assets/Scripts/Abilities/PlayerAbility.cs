using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float powerRadius = 3f;

    private int _powerLevel = 1;
    private Weather _currentAbility = Weather.None;

    private const KeyCode _keyCode1 = KeyCode.F;
    private const KeyCode _keyCode2 = KeyCode.JoystickButton1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_keyCode1) || Input.GetKeyDown(_keyCode2))
        {
            OnAbilityDown(_currentAbility);
        }

        if (Input.GetKey(_keyCode1) || Input.GetKey(_keyCode2))
        {
            OnAbility(_currentAbility);
        }

        if (Input.GetKeyUp(_keyCode1) || Input.GetKeyUp(_keyCode2))
        {
            OnAbilityUp(_currentAbility);
        }
    }

    // Set the currently active ability
    public void ActivateAbility(Weather ability)
    {
        _currentAbility = ability;
    }

    // Called first frame that ability is used
    void OnAbilityDown(Weather ability)
    {
        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        #region Call Button Down Method
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            switch(ability)
            {
                case Weather.Sun:
                    Sunable sunable = affectedObjects[i].GetComponent<Sunable>();
                    if (sunable != null)
                    {
                        sunable.OnSunDown(_powerLevel);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreezeDown(_powerLevel);
                    }
                    break;

                case Weather.Wind:
                    Windable windable= affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWindDown(_powerLevel);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable= affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRainDown(_powerLevel);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }


    // Called every frame (except first and last) that ability is used
    void OnAbility(Weather ability)
    {
        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        #region Call Button Stay Method
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            switch (ability)
            {
                case Weather.Sun:
                    Sunable sunable = affectedObjects[i].GetComponent<Sunable>();
                    if (sunable != null)
                    {
                        sunable.OnSun(_powerLevel);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreeze(_powerLevel);
                    }
                    break;

                case Weather.Wind:
                    Windable windable = affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWind(_powerLevel);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable = affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRain(_powerLevel);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }

    // Called last frame that ability is used
    void OnAbilityUp(Weather ability)
    {
        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        #region Call Button Up Method
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            switch (ability)
            {
                case Weather.Sun:
                    Sunable sunable = affectedObjects[i].GetComponent<Sunable>();
                    if (sunable != null)
                    {
                        sunable.OnSunUp(_powerLevel);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreezeUp(_powerLevel);
                    }
                    break;

                case Weather.Wind:
                    Windable windable = affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWindUp(_powerLevel);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable = affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRainUp(_powerLevel);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}

public enum Weather { None, Sun, Frost, Wind, Rain};
