using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherHUDPanel : MonoBehaviour
{
    public Image panelImage;
    public Image Icon;

    public void SetAsActivePanel(float scaleFactor)
    {
        panelImage.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    public void SetAsInActivePanel()
    {
        panelImage.rectTransform.localScale = new Vector3(1, 1, 1);
    }

    public void SetIconEnabled(bool enabled)
    {
        Icon.enabled = enabled;
    }
}
