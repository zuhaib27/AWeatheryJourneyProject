using Assets.Player.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collectiblePickUp : MonoBehaviour
{
    public AudioSource collectSound;
    //public SpriteRenderer notCollectedHat;
    //public SpriteRenderer collectedHat;
    // Start is called before the first frame update
    void Start()
    {
       // collectSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(15 * Time.deltaTime, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ScoringSystem.theScore++; //increase score count
            //notCollectedHat.enabled = false;
            //collectedHat.enabled = true;
            ScoringSystem.hatCollected = true;
            
            collectSound.Play(0); //need to add soundfx 
            this.gameObject.SetActive(false);
        }
    }
}
