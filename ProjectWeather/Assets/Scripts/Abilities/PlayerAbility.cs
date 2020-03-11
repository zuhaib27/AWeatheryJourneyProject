using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

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
    
    void Update()
    {
        // Update active ability
        if (ButtonMappings.GetButtonDown(Button.WindActivate))
            ActivateAbility(Weather.Wind);

        if (ButtonMappings.GetButtonDown(Button.RainActivate))
            ActivateAbility(Weather.Rain);

        if (ButtonMappings.GetButtonDown(Button.FrostActivate))
            ActivateAbility(Weather.Frost);

        if (ButtonMappings.GetButtonDown(Button.SunActivate))
            ActivateAbility(Weather.Sun);


        // Update when the player uses the ability
        bool isPressed = false;

        if (ButtonMappings.GetButtonDown(Button.AbilityUse))
        {
            OnAbilityDown(_currentAbility);
            isPressed = true;
        }

        if (ButtonMappings.GetButton(Button.AbilityUse))
        {
            OnAbility(_currentAbility);
            isPressed = true;
        }

        if (ButtonMappings.GetButtonUp(Button.AbilityUse))
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
            return _currentAbility == ability && ButtonMappings.GetButtonDown(Button.AbilityUse);
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
