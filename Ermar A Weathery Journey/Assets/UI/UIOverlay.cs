using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
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
                    _activeImage.gameObject.SetActive(false);
                iceIcon.gameObject.SetActive(true);
                _activeImage = iceIcon;
                break;
            case Weather.Sun:
                if (_activeImage)
                    _activeImage.gameObject.SetActive(false);
                sunIcon.gameObject.SetActive(true);
                _activeImage = sunIcon;
                break;
            case Weather.Wind:
                if (_activeImage)
                    _activeImage.gameObject.SetActive(false);
                windIcon.gameObject.SetActive(true);
                _activeImage = windIcon;
                break;
            case Weather.Rain:
                if (_activeImage)
                    _activeImage.gameObject.SetActive(false);
                rainIcon.gameObject.SetActive(true);
                _activeImage = rainIcon;
                break;
            default:
                Debug.LogWarning("UI Overlay does not support weather type.");
                break;
        }
    }
}
