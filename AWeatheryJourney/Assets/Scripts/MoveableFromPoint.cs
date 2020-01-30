using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableFromPoint : Moveable
{
    public Transform grabPoint;
    public float grabRadius = .5f;

    // Override valid grabbing range to only include radius around the grabbing point
    public override bool IsInGrabRange(Transform player)
    {
        if (!base.IsInGrabRange(player))
            return false;

        bool isWithinRadius = false;

        if (Vector3.Distance(player.position, grabPoint.position) < grabRadius)
            isWithinRadius = true;

        return isWithinRadius;
    }
}
