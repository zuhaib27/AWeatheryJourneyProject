using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour
{
    public GameObject RaisedBucket;
    public GameObject LowerBucket;
    public Transform minBucketPos;
    public float verticalSpeed = 5f;
    public bool enableRain;
    // Start is called before the first frame update
    void Start()
    {
        enableRain = false;
    }

    private void Update()
    {
        if (enableRain)
        {
            RaiseLowerBuckets();
        }
    }

    // Update is called once per frame
    public void RaiseLowerBuckets()
    {
        if(!(RaisedBucket.transform.position.y <= minBucketPos.position.y))
        {
            //RaisedBucket.transform.position = new Vector3(RaisedBucket.transform.position.x,
            //(RaisedBucket.transform.position.y - _waterLevel), RaisedBucket.transform.position.z);

            RaisedBucket.transform.position += Vector3.down * verticalSpeed * Time.deltaTime;
            LowerBucket.transform.position += Vector3.up * verticalSpeed * Time.deltaTime;

            //            LowerBucket.transform.position = new Vector3(LowerBucket.transform.position.x,
            //              (LowerBucket.transform.position.y + _waterLevel), LowerBucket.transform.position.z);
        }

    }
}
