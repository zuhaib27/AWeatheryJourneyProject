using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AWeatheryJourney;

public class PlayerAbility : MonoBehaviour
{
    public float powerRadius = 3f;

    [Header("Wind Ability")]
    public float ImpulseMagnitude = 20f;
    public float WindPreGroundingGraceTime = 0f;

    [Header("Enabled Abilities")]
    public bool isWindEnabled = true;
    public bool isRainEnabled = true;
    public bool isSunEnabled = true;
    public bool isFrostEnabled = true;

    // Private variables
    private Weather _currentAbility = Weather.None;
    private bool _isBeingPressed = false;

    private SpellParticleEffects _spellEffects;
    private PlayerSounds _playerSounds;

    private WeatherHUD _weatherHUD;

    private void Awake()
    {
        _spellEffects = GetComponent<SpellParticleEffects>();
        _playerSounds = GetComponent<PlayerSounds>();
    }

    private void Start()
    {
        #region dependency checks
        _weatherHUD = FindObjectOfType<WeatherHUD>();
        if (!_weatherHUD)
            Debug.LogWarning("No WeatherHUD found in scene.");

        if (!MusicPlayer.Instance)
            Debug.LogWarning("No MusicPlayer found in scene.");
        #endregion

        if (CheckpointManager.Instance)
            CheckpointManager.Instance.OnSpawn += OnSpawn;
    }

    private void OnSpawn()
    {
        ActivateAbility(Weather.None);
    }
    
    void Update()
    {
        // Update active ability
        if (isWindEnabled && ButtonMappings.GetButtonDown(Button.WindActivate))
            ActivateAbility(Weather.Wind);

        if (isRainEnabled && ButtonMappings.GetButtonDown(Button.RainActivate))
            ActivateAbility(Weather.Rain);

        if (isFrostEnabled && ButtonMappings.GetButtonDown(Button.FrostActivate))
            ActivateAbility(Weather.Frost);

        if (isSunEnabled && ButtonMappings.GetButtonDown(Button.SunActivate))
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

    // Enable an ability (disabled abilties cannot be made active)
    public void EnableAbility(Weather ability)
    {
        switch (ability)
        {
            case Weather.Wind:
                isWindEnabled = true;
                break;
            case Weather.Rain:
                isRainEnabled = true;
                break;
            case Weather.Sun:
                isSunEnabled = true;
                break;
            case Weather.Frost:
                isFrostEnabled = true;
                break;
        }
    }

    // Set the currently active ability
    public void ActivateAbility(Weather ability)
    {
        _currentAbility = ability;
        if (_weatherHUD)
            _weatherHUD.SetUIIcon(ability);
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
        // Particles and sounds
        _spellEffects.UseEffect(ability);
        _playerSounds.PlaySpell(ability);

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
        // Particles and sounds
        _spellEffects.StopParticleEffect(ability);
        _playerSounds.StopSpell(ability);

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
