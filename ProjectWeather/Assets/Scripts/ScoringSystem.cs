using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;
    public Image notCollectedHatImg;
    public Image collectedHatImg;
    public static bool hatCollected = false;

    // Start is called before the first frame update
    void Update()
    {
        //scoreText.GetComponent<Text>().text = "Bonus Collected : " + theScore;
        if(hatCollected)
        {
            notCollectedHatImg.enabled = false;
            collectedHatImg.enabled = true;
        }
    }


}
