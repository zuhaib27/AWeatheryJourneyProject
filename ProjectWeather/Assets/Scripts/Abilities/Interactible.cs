using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public void OnAbilityDown(Weather ability, AbilityEvent e)
    {
        #region Call Button Down Method
        switch (ability)
        {
            case Weather.Sun:
                OnSunDown(e);
                break;

            case Weather.Frost:
                OnFreezeDown(e);
                break;

            case Weather.Wind:
                OnWindDown(e);
                break;

            case Weather.Rain:
                OnRainDown(e);
                break;

            default:
                break;
        }
        #endregion
    }
    
    public void OnAbility(Weather ability, AbilityEvent e)
    {
        Debug.Log("Interactible on ability");
        #region Call Button Stay Method
        switch (ability)
        {
            case Weather.Sun:
                OnSun(e);
                break;

            case Weather.Frost:
                OnFreeze(e);
                break;

            case Weather.Wind:
                OnWind(e);
                break;

            case Weather.Rain:
                OnRain(e);
                break;

            default:
                break;
        }
        #endregion
    }
    
    public void OnAbilityUp(Weather ability, AbilityEvent e)
    {
        #region Call Button Up Method
        switch (ability)
        {
            case Weather.Sun:
                OnSunUp(e);
                break;

            case Weather.Frost:
                OnFreezeUp(e);
                break;

            case Weather.Wind:
                OnWindUp(e);
                break;

            case Weather.Rain:
                OnRainUp(e);
                break;

            default:
                break;
            }
        #endregion
    }
    
    // Sun shorthand
    public virtual void OnSunDown(AbilityEvent e) { }
    public virtual void OnSun(AbilityEvent e) { }
    public virtual void OnSunUp(AbilityEvent e) { }

    // Frost shorthand
    public virtual void OnFreezeDown(AbilityEvent e) { }
    public virtual void OnFreeze(AbilityEvent e) { }
    public virtual void OnFreezeUp(AbilityEvent e) { }

    // Wind shorthand
    public virtual void OnWindDown(AbilityEvent e) { }
    public virtual void OnWind(AbilityEvent e) { }
    public virtual void OnWindUp(AbilityEvent e) { }

    // Rain shorthand
    public virtual void OnRainDown(AbilityEvent e) { }
    public virtual void OnRain(AbilityEvent e) { }
    public virtual void OnRainUp(AbilityEvent e) { }
}
