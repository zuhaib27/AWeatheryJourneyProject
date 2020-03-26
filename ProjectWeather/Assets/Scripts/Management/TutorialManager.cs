using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AWeatheryJourney;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }

    private GameObject _currentTip;
    private AWeatheryJourney.Button _waitButton;

    private void Awake()
    {
        #region singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            //Destroy(gameObject);
        }
        #endregion
    }

    public void Update()
    {
        if (_currentTip != null && ButtonMappings.GetButtonDown(_waitButton))
        {
            _currentTip.SetActive(false);
            _currentTip = null;
        }
    }

    public void DisplayTip(GameObject tip, AWeatheryJourney.Button waitButton)
    {
        if (_currentTip)
            _currentTip.SetActive(false);

        _currentTip = tip;
        _waitButton = waitButton;

        _currentTip.SetActive(true);
    }
}
