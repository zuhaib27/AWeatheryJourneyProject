using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MonoBehaviour
{
    private Ice _ice;

    private void Awake()
    {
        _ice = GetComponentInParent<Ice>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _ice.UpdateCollider();
    }
}
