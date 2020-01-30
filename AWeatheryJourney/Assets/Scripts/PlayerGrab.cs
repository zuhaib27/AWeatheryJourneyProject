using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public float grabRadius = 0.5f;
    public Transform grabPoint;

    private Moveable grabbedObject = null;
    private Vector3 positionOffset;
    private Quaternion rotationOffset;

    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        MoveObject();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            GrabObject();
        }

        if (Input.GetKeyUp("q"))
        {
            LetGoOfObject();
        }
    }

    // Search radius around grabPoint for moveable objects
    void GrabObject()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(grabPoint.position, grabRadius);

        for (int i = 0; i < nearbyObjects.Length; i++)
        {
            Moveable moveable = nearbyObjects[i].GetComponent<Moveable>();

            if (moveable != null && moveable.IsInGrabRange(transform))
            {
                Debug.Log("Grabbed object");
                grabbedObject = moveable;
                break;
            }
        }
    }

    // Let go of the currently held object
    void LetGoOfObject()
    {
        if (grabbedObject != null)
            Debug.Log("Let go of object");

        grabbedObject = null;
    }

    // Push or pull the held object
    void MoveObject()
    {
        if (grabbedObject != null)
        {
            Vector3 movement = playerMovement.GetPlayerSpeed();

            if (movement != Vector3.zero)
            {
                Vector3 directionToObject = (grabbedObject.transform.position - transform.position).normalized;
                bool isPushing = (Vector3.Dot(movement.normalized, directionToObject) > 0);

                if (isPushing)
                {
                    if (grabbedObject.IsPushable())
                    {
                        grabbedObject.Move(movement);
                    }
                    else
                    {
                        LetGoOfObject();
                    }
                }
                else
                {
                    if (grabbedObject.IsPullable())
                    {
                        grabbedObject.Move(movement);
                    }
                    else
                    {
                        LetGoOfObject();
                    }
                }
            }
        }
    }
}
