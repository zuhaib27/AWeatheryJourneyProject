using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerSounds : MonoBehaviour
{
    public Sound footstep;

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
}
