using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{
    public Sound footstep;

    public AudioSource windSpell;
    public AudioSource rainSpell;
    public AudioSource sunSpell;
    public AudioSource frostSpell;

    private Assets.Player.Scripts.MyCharacterController _characterController;

    private TerrainDetector _terrainDetector;

    private void Awake()
    {
        _characterController = GetComponent<Assets.Player.Scripts.MyCharacterController>();
        _terrainDetector = new TerrainDetector();
    }

    public void PlayStep() 
    {
        if (!_characterController.IsPlayerOnGround())
            return;

        // terrain not yet implemented
        int terrainTextureIndex = 0;// _terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        switch (terrainTextureIndex)
        {
            case 0:
                footstep.Play();
                break;
            case 1:
                footstep.Play();
                break;
            case 2:
            default:
                footstep.PlayOneShot();
                break;
        }

    }

    public void PlayLanding()
    {
        if (!_characterController.IsPlayerOnGround())
            return;

        footstep.PlayOneShot(footstep.Volume * 1.2f);
    }

    public void PlaySpell(Weather spell)
    {
        switch (spell)
        {
            case Weather.Wind:
                if (!windSpell.isPlaying)
                    windSpell.Play();
                break;
            case Weather.Rain:
                rainSpell.Play();
                break;
            case Weather.Sun:
                sunSpell.Play();
                break;
            case Weather.Frost:
                frostSpell.Play();
                break;
        }
    }

    public void StopSpell(Weather spell)
    {
        switch (spell)
        {
            case Weather.Wind:
                //windSpell.Stop();
                break;
            case Weather.Rain:
                rainSpell.Stop();
                break;
            case Weather.Sun:
                sunSpell.Stop();
                break;
            case Weather.Frost:
                frostSpell.Stop();
                break;
        }
    }
}
