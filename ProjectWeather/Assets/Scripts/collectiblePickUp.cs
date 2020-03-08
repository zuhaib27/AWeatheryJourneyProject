using Assets.Player.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectiblePickUp : MonoBehaviour
{
    public AudioSource collectSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoringSystem.theScore++;
            //collectSound.Play(); //need to add soundfx 
            Destroy(gameObject);
        }
    }
}
