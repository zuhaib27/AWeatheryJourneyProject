using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{
    public Animator animator;

    public float scaleFactor = 1.5f;

    public Image iceIcon;
    public Image sunIcon;
    public Image windIcon;
    public Image rainIcon;

    private Image _activeImage;

    public void SetUIIcon(Weather weather)
    {
        switch (weather)
        {
            case Weather.Frost:
                if (_activeImage)
                    _activeImage.rectTransform.localScale = new Vector3(1, 1, 1);
                iceIcon.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _activeImage = iceIcon;
                break;
            case Weather.Sun:
                if (_activeImage)
                    _activeImage.rectTransform.localScale = new Vector3(1, 1, 1);
                sunIcon.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _activeImage = sunIcon;
                break;
            case Weather.Wind:
                if (_activeImage)
                    _activeImage.rectTransform.localScale = new Vector3(1, 1, 1);
                windIcon.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _activeImage = windIcon;
                break;
            case Weather.Rain:
                if (_activeImage)
                    _activeImage.rectTransform.localScale = new Vector3(1, 1, 1);
                rainIcon.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                _activeImage = rainIcon;
                break;
            default:
                Debug.LogWarning("UI Overlay does not support weather type.");
                break;
        }
    }
}
