using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucketPlatformAttach : MonoBehaviour
{
    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontop of bucket of water");
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
            this.transform.parent.GetComponent<PulleySystem>().enableRain = false;
        }
    }
}
