using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Useful script for determining where points in local space are in the world space
public class DrawObjectPoints : MonoBehaviour
{
    // Draw points in editor
    private void OnDrawGizmos()
    {
        Vector3 origin = transform.TransformPoint(0, 0, 0);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, .1f);

        Vector3 corner1 = transform.TransformPoint(.5f, .5f, .5f);
        Vector3 corner2 = transform.TransformPoint(.5f, .5f, -.5f);
        Vector3 corner3 = transform.TransformPoint(.5f, -.5f, .5f);
        Vector3 corner4 = transform.TransformPoint(.5f, -.5f, -.5f);
        Vector3 corner5 = transform.TransformPoint(-.5f, .5f, .5f);
        Vector3 corner6 = transform.TransformPoint(-.5f, .5f, -.5f);
        Vector3 corner7 = transform.TransformPoint(-.5f, -.5f, .5f);
        Vector3 corner8 = transform.TransformPoint(-.5f, -.5f, -.5f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(corner1, .1f);
        Gizmos.DrawWireSphere(corner2, .1f);
        Gizmos.DrawWireSphere(corner3, .1f);
        Gizmos.DrawWireSphere(corner4, .1f);
        Gizmos.DrawWireSphere(corner5, .1f);
        Gizmos.DrawWireSphere(corner6, .1f);
        Gizmos.DrawWireSphere(corner7, .1f);
        Gizmos.DrawWireSphere(corner8, .1f);
    }
}
