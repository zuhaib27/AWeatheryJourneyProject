using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherHUD : MonoBehaviour
{
    public float scaleFactor = 1.5f;

    public WeatherHUDPanel windPanel;
    public WeatherHUDPanel rainPanel;
    public WeatherHUDPanel sunPanel;
    public WeatherHUDPanel frostPanel;

    private WeatherHUDPanel _activePanel;

    public void Awake()
    {
        PlayerAbility playerAbility = FindObjectOfType<PlayerAbility>();

        windPanel.SetIconEnabled(playerAbility.isWindEnabled);
        rainPanel.SetIconEnabled(playerAbility.isRainEnabled);
        sunPanel.SetIconEnabled(playerAbility.isSunEnabled);
        frostPanel.SetIconEnabled(playerAbility.isFrostEnabled);
    }

    public void EnableUIIcon(Weather weather)
    {
        switch (weather)
        {
            case Weather.Wind:
                windPanel.SetIconEnabled(true);
                break;
            case Weather.Rain:
                rainPanel.SetIconEnabled(true);
                break;
            case Weather.Sun:
                sunPanel.SetIconEnabled(true);
                break;
            case Weather.Frost:
                frostPanel.SetIconEnabled(true);
                break;
        }
    }

    public void SetUIIcon(Weather weather)
    {
        switch (weather)
        {
            case Weather.Wind:
                if (_activePanel)
                    _activePanel.SetAsInActivePanel();
                windPanel.SetAsActivePanel(scaleFactor);
                _activePanel = windPanel;
                break;
            case Weather.Rain:
                if (_activePanel)
                    _activePanel.SetAsInActivePanel();
                rainPanel.SetAsActivePanel(scaleFactor);
                _activePanel = rainPanel;
                break;
            case Weather.Sun:
                if (_activePanel)
                    _activePanel.SetAsInActivePanel();
                sunPanel.SetAsActivePanel(scaleFactor);
                _activePanel = sunPanel;
                break;
            case Weather.Frost:
                if (_activePanel)
                    _activePanel.SetAsInActivePanel();
                frostPanel.SetAsActivePanel(scaleFactor);
                _activePanel = frostPanel;
                break;
        }
    }
}
