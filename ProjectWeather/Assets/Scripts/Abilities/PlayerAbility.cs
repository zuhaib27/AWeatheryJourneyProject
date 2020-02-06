using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float powerRadius = 3f;

    int powerLevel = 1;
    Weather currentAbility = Weather.None;

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton1))
        //{
        //    ActivateAbility(Weather.Frost);
        //}
        //else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton2))
        //{
        //    ActivateAbility(Weather.Sun);
        //}
        //else if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.JoystickButton3))
        //{
        //    ActivateAbility(Weather.Rain);
        //}

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            UseAbility(currentAbility);
        }
    }

    // Set the currently active ability
    public void ActivateAbility(Weather ability)
    {
        currentAbility = ability;
    }

    // Use the given ability
    void UseAbility(Weather ability)
    {
        Debug.Log("Using ability " + ability);
        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);
    

        for (int i = 0; i < affectedObjects.Length; i++)
        {
            switch(ability)
            {
                case Weather.Sun:
                    Sunable sunable = affectedObjects[i].GetComponent<Sunable>();
                    if (sunable != null)
                    {
                        sunable.OnSun(powerLevel);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreeze(powerLevel);
                    }
                    break;

                case Weather.Wind:
                    Windable windable= affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWind(powerLevel);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable= affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRain(powerLevel);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}

public enum Weather { None, Sun, Frost, Wind, Rain};
