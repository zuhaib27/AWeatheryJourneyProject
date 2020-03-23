using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MonoBehaviour
{
    public IceGridMesh _iceGrid;
    public MeshCollider _icePlane;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Water - Collision");

            bool isOnIce = _iceGrid.Intersects(collision.GetContact(0).point);
            _icePlane.enabled = isOnIce;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Water - Trigger");

            bool isOnIce = _iceGrid.Intersects(other.gameObject.transform.position);
            _icePlane.enabled = isOnIce;
        }
    }
}
