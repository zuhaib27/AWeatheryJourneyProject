using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    // Called first frame that ability is used
    public void OnAbilityDown(Weather ability, AbilityEvent e)
    {
        #region Call Button Down Method
        switch (ability)
        {
            case Weather.Sun:
                Sunable sunable = this.GetComponent<Sunable>();
                if (sunable != null)
                {
                    sunable.OnSunDown(e);
                }
                break;

            case Weather.Frost:
                Freezeable freezeable = this.GetComponent<Freezeable>();
                if (freezeable != null)
                {
                    freezeable.OnFreezeDown(e);
                }
                break;

            case Weather.Wind:
                Windable windable = this.GetComponent<Windable>();
                if (windable != null)
                {
                    windable.OnWindDown(e);
                }
                break;

            case Weather.Rain:
                Rainable rainable = this.GetComponent<Rainable>();
                if (rainable != null)
                {
                    rainable.OnRainDown(e);
                }
                break;

            default:
                break;
        }
        #endregion
    }


    // Called every frame (except first and last) that ability is used
    public void OnAbility(Weather ability, AbilityEvent e)
    {
        #region Call Button Stay Method
        switch (ability)
        {
            case Weather.Sun:
                Sunable sunable = this.GetComponent<Sunable>();
                if (sunable != null)
                {
                    sunable.OnSun(e);
                }
                break;

            case Weather.Frost:
                Freezeable freezeable = this.GetComponent<Freezeable>();
                if (freezeable != null)
                {
                    freezeable.OnFreeze(e);
                }
                break;

            case Weather.Wind:
                Windable windable = this.GetComponent<Windable>();
                if (windable != null)
                {
                    windable.OnWind(e);
                }
                break;

            case Weather.Rain:
                Rainable rainable = this.GetComponent<Rainable>();
                if (rainable != null)
                {
                    rainable.OnRain(e);
                }
                break;

            default:
                break;
        }
        #endregion
    }

    // Called last frame that ability is used
    public void OnAbilityUp(Weather ability, AbilityEvent e)
    {
        #region Call Button Up Method
        switch (ability)
        {
            case Weather.Sun:
                Sunable sunable = this.GetComponent<Sunable>();
                if (sunable != null)
                {
                    sunable.OnSunUp(e);
                }
                break;

            case Weather.Frost:
                Freezeable freezeable = this.GetComponent<Freezeable>();
                if (freezeable != null)
                {
                    freezeable.OnFreezeUp(e);
                }
                break;

            case Weather.Wind:
                Windable windable = this.GetComponent<Windable>();
                if (windable != null)
                {
                    windable.OnWindUp(e);
                }
                break;

            case Weather.Rain:
                Rainable rainable = this.GetComponent<Rainable>();
                if (rainable != null)
                {
                    rainable.OnRainUp(e);
                }
                break;

            default:
                break;
            }
        #endregion
    }
}
