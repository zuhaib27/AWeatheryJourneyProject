using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrigger : MonoBehaviour
{
    public IceGridMesh _iceGrid;
    public MeshCollider _icePlane;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool isOnIce = _iceGrid.Intersects(other.gameObject.transform.position);
            _icePlane.enabled = isOnIce;
        }
    }
}
