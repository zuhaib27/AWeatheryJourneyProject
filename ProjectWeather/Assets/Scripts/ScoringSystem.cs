using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static int theScore;

    // Start is called before the first frame update
    void Update()
    {
        scoreText.GetComponent<Text>().text = "Bonus Collected : " + theScore;
    }


}
