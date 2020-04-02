using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunKey : Interactible
{
    public int keyIndex = 0;
    public GameObject keyLight;

    public float audioStartSeconds = 0f;

    public override void OnSun(AbilityEvent e)
    {
        base.OnSun(e);

        if (!LockedSunWallController.instance.sunKey[keyIndex])
        {
            Debug.Log("activated key");
            keyLight.SetActive(true);
            LockedSunWallController.instance.sunKey[keyIndex] = true;
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
            audio.time = audioStartSeconds;
        }
    }
}
