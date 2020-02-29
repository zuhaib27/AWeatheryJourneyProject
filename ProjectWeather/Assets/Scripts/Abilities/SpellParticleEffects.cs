using System.Diagnostics.Tracing;
using UnityEngine;


//public enum Effect { Playing, Stopped };
public enum State
{
    None,
    Activate,
    Use
};

public class SpellParticleEffects : MonoBehaviour
{
    [Header("Activate Ability Effects")]
    public ParticleSystem sunParticleActivate;
    public ParticleSystem rainParticleActivate;
    public ParticleSystem frostParticleActivate;
    public ParticleSystem windParticleActivate;

    [Header("Use Ability Effects")]
    public ParticleSystem sunParticleUse;
    public ParticleSystem rainParticleUse;
    public ParticleSystem frostParticleUse;
    public ParticleSystem windParticleUse;

    // Private variables
    //private Effect _effect = Effect.Stopped;
    private State _state = State.None;
    private Weather _currentWeather = Weather.None;
    private ParticleSystem _activeParticle;


    private void Start()
    {
        // Initialize _activeParticle
        _activeParticle = windParticleUse;
    }


    public void ActivateEffect(Weather ability)
    {
        // If this Weather Ability is already active, then return
        if (_currentWeather == ability)
        {
            return;
        }
        // Otherwise clear the other particle effects
        else
        {
            StopParticleEffect();
            ClearParticleEffects();
        }

        switch (ability)
        {
            case Weather.Sun:
                _activeParticle = sunParticleActivate;
                break;
            case Weather.Rain:
                _activeParticle = rainParticleActivate;
                break;
            case Weather.Frost:
                _activeParticle = frostParticleActivate;
                break;
            case Weather.Wind:
                _activeParticle = windParticleActivate;
                break;
            default:
                break;
        }

        _currentWeather = ability;
        _state = State.Activate;
        PlayParticleEffect();
    }

    public void UseEffect(Weather ability)
    {
        // Prevent spamming the same active spell effect
        if (_state == State.Use && _activeParticle.isPlaying)
        {
            return;
        }

        _state = State.Use;

        switch (ability)
        {
            case Weather.Sun:
                _activeParticle = sunParticleUse;
                break;
            case Weather.Rain:
                _activeParticle = rainParticleUse;
                break;
            case Weather.Frost:
                _activeParticle = frostParticleUse;
                break;
            case Weather.Wind:
                _activeParticle = windParticleUse;
                break;
            default:
                break;
        }

        PlayParticleEffect();
    }

    private void ClearParticleEffects()
    {
        _activeParticle.Clear(true);
    }

    public void StopParticleEffect()
    {
        _activeParticle.Stop(true);
    }

    private void PlayParticleEffect()
    {
        _activeParticle.Play(true);
    }

    private void FindParticleSystem(Weather ability)
    {
        
    }
}
