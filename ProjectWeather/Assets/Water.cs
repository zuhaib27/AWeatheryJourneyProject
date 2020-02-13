using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    IceGenerator _iceGenerator;

    private void Start()
    {
        _iceGenerator = GetComponentInChildren<IceGenerator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger water");
        _iceGenerator.UpdateCollider();
    }
}
