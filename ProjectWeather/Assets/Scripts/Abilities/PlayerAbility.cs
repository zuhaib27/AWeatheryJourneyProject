using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    public float powerRadius = 3f;

    public WeatherUI weatherUI;

    [Header("Wind Ability")]
    public bool AllowWindAbility = true;
    public float ImpulseMagnitude = 20f;
    public float WindPreGroundingGraceTime = 0f;

    // Private variables
    private SpellParticleEffects _spellEffects;
    private Weather _currentAbility = Weather.None;
    private bool _isBeingPressed = false;

    private const KeyCode _keyCode1 = KeyCode.F;
    private const KeyCode _keyCode2 = KeyCode.JoystickButton1;  // B on Xbox controller

    private string _button3 = "DPad Vertical";
    private string _button4 = "DPad Horizontal";


    private void Awake()
    {
        _spellEffects = GetComponent<SpellParticleEffects>();
    }

    private void Start()
    {
        #region dependency checks
        if (!weatherUI)
            Debug.LogWarning("No WeatherUI found in scene.");

        if (!MusicPlayer.Instance)
            Debug.LogWarning("No MusicPlayer found in scene.");
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        // Update D-pad active ability
        if (Input.GetAxisRaw(_button3) > 0 || Input.GetKeyDown(KeyCode.Alpha1))
        {
            ActivateAbility(Weather.Wind);
        }
        else if (Input.GetAxisRaw(_button3) < 0 || Input.GetKeyDown(KeyCode.Alpha2))
        {
            ActivateAbility(Weather.Rain);
        }

        if (Input.GetAxisRaw(_button4) > 0 || Input.GetKeyDown(KeyCode.Alpha3))
        {
            ActivateAbility(Weather.Frost);
        }
        else if (Input.GetAxisRaw(_button4) < 0 || Input.GetKeyDown(KeyCode.Alpha4))
        {
            ActivateAbility(Weather.Sun);
        }


        // Update when the player uses the ability
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
        if (ability == Weather.Wind)
        {
            return Input.GetKeyDown(_keyCode2) && _currentAbility == ability;
        }

        return _isBeingPressed && (_currentAbility == ability);
    }

    // Set the currently active ability
    public void ActivateAbility(Weather ability)
    {
        _currentAbility = ability;
        if (weatherUI)
            weatherUI.SetUIIcon(ability);
        if (MusicPlayer.Instance)
            MusicPlayer.Instance.SwitchSong((int)ability);

        // Particle effects
        _spellEffects.ActivateEffect(ability);
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
        // Start particle effect
        _spellEffects.UseEffect(ability);

        AbilityEvent e = CreateAbilityEvent();

        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        for (int i = 0; i < affectedObjects.Length; i++)
        {
            Interactible interactible = affectedObjects[i].GetComponent<Interactible>();
            if (interactible != null)
            {
                interactible.OnAbilityDown(ability, e);
            }
        }
    }

    // Called every frame (except first and last) that ability is used
    void OnAbility(Weather ability)
    {
        AbilityEvent e = CreateAbilityEvent();

        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        for (int i = 0; i < affectedObjects.Length; i++)
        {
            Interactible interactible = affectedObjects[i].GetComponent<Interactible>();
            if (interactible != null)
            {
                interactible.OnAbility(ability, e);
            }
        }
    }

    // Called last frame that ability is used
    void OnAbilityUp(Weather ability)
    {
        _spellEffects.StopParticleEffect(ability);

        AbilityEvent e = CreateAbilityEvent();

        Physics.SyncTransforms(); //not certain if need this, but was recomended to keep track of other objects...
        Collider[] affectedObjects = Physics.OverlapSphere(transform.position, powerRadius);

        for (int i = 0; i < affectedObjects.Length; i++)
        {
            Interactible interactible = affectedObjects[i].GetComponent<Interactible>();
            if (interactible != null)
            {
                interactible.OnAbilityUp(ability, e);
            }
        }
    }
}

// Weather enum
public enum Weather { None, Sun, Frost, Wind, Rain};

// The event that will be sent to objects that are affected by an ability
public struct AbilityEvent
{
    public Vector3 playerPosition;
}















// Removed. Was part of particle effects
// Helper to time light effect
//private IEnumerator LightCall()
//{
    //yield return new WaitForSeconds(sunlightDuration);
    //sunLight.enabled = false;
//}
