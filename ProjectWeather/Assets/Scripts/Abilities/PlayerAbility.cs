using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    int powerLevel = 1;
    float powerRadius = 3f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActivateAbility(Weather.Frost);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            ActivateAbility(Weather.Sun);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ActivateAbility(Weather.Rain);
        }
    }

    void ActivateAbility(Weather ability)
    {
        Debug.Log("Activated ability");
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
                    break;

                case Weather.Rain:
                    break;

                default:
                    break;
            }
        }
    }
}

enum Weather { Sun, Frost, Wind, Rain};
