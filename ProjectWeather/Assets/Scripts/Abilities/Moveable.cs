using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public bool isPushable;
    public bool isPullable;

    public float heaviness = 1f;
    public float facingAngleTolerance = 90f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Check if the given player is in range to grab this object
    public virtual bool IsInGrabRange(Transform player)
    {
        bool isFacingTarget = false;

        float angleFromPlayerToTarget = Vector3.Angle(player.forward, transform.position - player.position);
        if (angleFromPlayerToTarget < facingAngleTolerance)
            isFacingTarget = true;

        return isFacingTarget;
    }

    // Check if this object can be pushed
    public virtual bool IsPushable()
    {
        return isPushable;
    }

    // Check if this object can be pulled
    public virtual bool IsPullable()
    {
        return isPullable;
    }

    // Move this object with a desired velocity (will be affected by object heaviness)
    public virtual void Move(Vector3 velocity)
    {
        Vector3 force = (velocity - rb.velocity) * rb.mass / Time.deltaTime;
        rb.AddForce(force / heaviness, ForceMode.Force);
    }
}
