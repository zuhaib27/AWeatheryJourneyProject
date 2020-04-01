using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shroom : Interactible
{
    public float sunMultiplier = 1.2f;
    public float maxPlayerDistance = 20f;

    [Header("Glow")]
    public Vector2 matGlowRange = new Vector2(.2f, 1f);
    public Vector2 lightIntensityRange = new Vector2(.1f, .5f);

    [Header("Particles")]
    public Vector2 particleMainEmissionRange = new Vector2(1f, 20f);
    public Vector2 particleMainVelocityRange = new Vector2(.1f, 1f);
    public Vector2 particleGlowEmmissionRange = new Vector2(.5f, 2.5f);
    public Vector2 particleCloudEmissionRange = new Vector2(1f, 25f);

    [Header("Other")]
    public ParticleSystem mainParticles;
    public ParticleSystem glowParticles;
    public ParticleSystem cloudParticles;

    public Light light;

    private bool _isSunActive = false;
    private float _playerDistance = 0f;
    private float _size;

    private Transform _player;
    private Material _mat;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerAbility>().transform;
        _mat = GetComponent<MeshRenderer>().material;
        _size = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z ) / 300f;
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
            glow = playerMultiplier / sunMultiplier;
            glow = Mathf.Clamp(glow, 0f, 1f);
        }

        float matGlow = matGlowRange[0] + (matGlowRange[1] - matGlowRange[0]) * glow;
        Color baseColor = _mat.GetColor("_BaseColor");
        _mat.SetColor("_EmissionColor", matGlow * baseColor);

        light.intensity = lightIntensityRange[0] + (lightIntensityRange[1] - lightIntensityRange[0]) * glow;
        light.intensity *= _size;


        // Main particles - emission
        float particleMainEmission = particleMainEmissionRange[0] + (particleMainEmissionRange[1] - particleMainEmissionRange[0]) * glow;
        particleMainEmission *= _size;
        var particleMainEmissionModule = mainParticles.emission;
        particleMainEmissionModule.rateOverTimeMultiplier = particleMainEmission;
        // Main particles - velocity
        var particleMainVelocityModule = mainParticles.velocityOverLifetime;
        float particleMainVelocity = particleMainVelocityRange[0] + (particleMainVelocityRange[1] - particleMainVelocityRange[0]) * glow;
        particleMainVelocityModule.speedModifier = new ParticleSystem.MinMaxCurve(particleMainVelocity, AnimationCurve.Constant(0, 1, 1));

        // Glow particles - emission
        float particleGlowEmission = particleGlowEmmissionRange[0] + (particleGlowEmmissionRange[1] - particleGlowEmmissionRange[0]) * glow;
        particleGlowEmission *= _size;
        var particleGlowEmissionModule = glowParticles.emission;
        particleGlowEmissionModule.rateOverTimeMultiplier = particleGlowEmission;
        // Glow particles - size
        //var particleGlowModule = glowParticles.main;
        //particleGlowModule.startSize = new ParticleSystem.MinMaxCurve(particleGlow * _size, AnimationCurve.Constant(0, 1, 1));

        // Cloud particles - emission
        float particleCloudEmission = particleCloudEmissionRange[0] + (particleCloudEmissionRange[1] - particleCloudEmissionRange[0]) * glow;
        particleCloudEmission *= _size;
        var particleCloudEmissionModule = cloudParticles.emission;
        particleCloudEmissionModule.rateOverTimeMultiplier = particleCloudEmission;

        _isSunActive = false;
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
