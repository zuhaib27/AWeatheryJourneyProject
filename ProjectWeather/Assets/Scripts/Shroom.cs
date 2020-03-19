using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : Interactible
{
    public float lightIntensity = .5f;
    public float idleGlow = .2f;
    public float sunMultiplier = 2f;
    public float maxPlayerDistance = 2f;

    public ParticleSystem mainParticles;
    public ParticleSystem glowParticles;
    public ParticleSystem cloudParticles;

    public Light light;

    private bool _isSunActive = false;
    private float _playerDistance = 0f;

    private Transform _player;

    private void Awake()
    {
        //var glow = glowParticles.lights;
        //glow.intensity = 0;

        //var mat = GetComponent<Material>();
        //mat.set("_TintColor", Color.black);
        _player = FindObjectOfType<PlayerAbility>().transform;
    }

    private void Update()
    {
        _playerDistance = Vector3.Distance(_player.position, transform.position);
        float playerMultiplier = Mathf.Clamp(1f - _playerDistance / maxPlayerDistance, 0f, 1f);
        float glow;

        if (_isSunActive)
        {
            glow = 1f;
        }
        else
        {
            glow = idleGlow + (1f - idleGlow) / sunMultiplier * playerMultiplier;
        }

        GetComponent<MeshRenderer>().material.SetColor("_TintColor", Color.white * glow);
        light.intensity = glow * lightIntensity;
        var particleVelocity = mainParticles.velocityOverLifetime;
        particleVelocity.speedModifier = new ParticleSystem.MinMaxCurve(glow * .8f, AnimationCurve.Constant(0, 1, 1));
        var particleGlow = glowParticles.lights;
        particleGlow.intensityMultiplier = glow * 3f;

        _isSunActive = false;
        //Debug.Log("Glow: " + glow);
    }

    public override void OnSunDown(AbilityEvent e)
    {
        base.OnSunDown(e);

        _isSunActive = true;
    }

    public override void OnSunUp(AbilityEvent e)
    {
        base.OnSunDown(e);

        _isSunActive = false;
    }

    public override void OnSun(AbilityEvent e)
    {
        base.OnSun(e);
        _isSunActive = true;

        //Debug.Log("Player dist: " + _playerDistance);
    }
}
