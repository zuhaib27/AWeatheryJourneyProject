using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float powerRadius = 3f;

    public UIOverlay uiOverlay;
    
    private Weather _currentAbility = Weather.None;
    private bool _isBeingPressed = false;

    private const KeyCode _keyCode1 = KeyCode.F;
    private const KeyCode _keyCode2 = KeyCode.JoystickButton1;
    
    public ParticleSystem sunParticle;
    public ParticleSystem rainParticle;
    public ParticleSystem snowParticle;
    public ParticleSystem windParticle;

    private string _button3 = "DPad Vertical";
    private string _button4 = "DPad Horizontal";

    private void Start()
    {
        #region dependency checks
        if (!uiOverlay)
            Debug.LogWarning("No UIOverlay found in scene.");

        if (!MusicPlayer.Instance)
            Debug.LogWarning("No MusicPlayer found in scene.");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw(_button3) > 0)
        {
            ActivateAbility(Weather.Wind);
        }
        else if (Input.GetAxisRaw(_button3) < 0)
        {
            ActivateAbility(Weather.Rain);
        }
        
        if (Input.GetAxisRaw(_button4) > 0)
        {
            ActivateAbility(Weather.Frost);
        }
        else if (Input.GetAxisRaw(_button4) < 0)
        {
            ActivateAbility(Weather.Sun);
        }

        bool isPressed = false;

        if (Input.GetKeyDown(_keyCode1) || Input.GetKeyDown(_keyCode2))
        {
            OnAbilityDown(_currentAbility);
            isPressed = true;
        }

        if (Input.GetKey(_keyCode1) || Input.GetKey(_keyCode2))
        {
            OnAbility(_currentAbility);
            isPressed = true;
        }

        if (Input.GetKeyUp(_keyCode1) || Input.GetKeyUp(_keyCode2))
        {
            OnAbilityUp(_currentAbility);
            isPressed = true;
        }

        _isBeingPressed = isPressed;
    }

    public bool IsAbilityBeingPressed(Weather ability)
    {
        return _isBeingPressed && (_currentAbility == ability);
    }

    // Set the currently active ability
    public void ActivateAbility(Weather ability)
    {
        _currentAbility = ability;
        if (uiOverlay)
            uiOverlay.SetUIIcon(ability);
        if (MusicPlayer.Instance)
            MusicPlayer.Instance.SwitchSong((int)ability);
    }

    // Create a player ability event for sending to interactable objects
    AbilityEvent CreateAbilityEvent()
    {
        AbilityEvent e = new AbilityEvent();
        e.playerPosition = transform.position;

        return e;
    }

    // Called first frame that ability is used
    void OnAbilityDown(Weather ability)
    {
        AbilityEvent e = CreateAbilityEvent();

        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        #region Call Button Down Method
        for (int i = 0; i < affectedObjects.Length; i++)
        {
            switch(ability)
            {
                case Weather.Sun:
                    sunParticle.Play();
                    Sunable sunable = affectedObjects[i].GetComponent<Sunable>();
                    if (sunable != null)
                    {
                        sunable.OnSunDown(e);
                    }
                    
                    break;

                case Weather.Frost:
                    snowParticle.Play();
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreezeDown(e);
                    }
                    break;

                case Weather.Wind:
                    windParticle.Play();
                    Windable windable= affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWindDown(e);
                    }
                    break;

                case Weather.Rain:
                    rainParticle.Play();
                    Rainable rainable= affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRainDown(e);
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
        AbilityEvent e = CreateAbilityEvent();

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
                        sunable.OnSun(e);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreeze(e);
                    }
                    break;

                case Weather.Wind:
                    Windable windable = affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWind(e);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable = affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRain(e);
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
        AbilityEvent e = CreateAbilityEvent();

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
                        sunable.OnSunUp(e);
                    }
                    break;

                case Weather.Frost:
                    Freezeable freezeable = affectedObjects[i].GetComponent<Freezeable>();
                    if (freezeable != null)
                    {
                        freezeable.OnFreezeUp(e);
                    }
                    break;

                case Weather.Wind:
                    Windable windable = affectedObjects[i].GetComponent<Windable>();
                    if (windable != null)
                    {
                        windable.OnWindUp(e);
                    }
                    break;

                case Weather.Rain:
                    Rainable rainable = affectedObjects[i].GetComponent<Rainable>();
                    if (rainable != null)
                    {
                        rainable.OnRainUp(e);
                    }
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}

// Weather enum
public enum Weather { None, Sun, Frost, Wind, Rain};

// The event that will be sent to objects that are affected by an ability
public struct AbilityEvent
{
    public Vector3 playerPosition;
}
