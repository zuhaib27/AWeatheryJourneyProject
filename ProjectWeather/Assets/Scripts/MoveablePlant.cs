using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class MoveablePlant : MonoBehaviour, IMoverController
{
    public PhysicsMover Mover;

    public Vector3 TranslationAxis = Vector3.right;
    public float TranslationPeriod = 10;
    public float TranslationSpeed = 1;
    public Vector3 RotationAxis = Vector3.up;
    public float RotSpeed = 10;
    public Vector3 OscillationAxis = Vector3.zero;
    public float OscillationPeriod = 10;
    public float OscillationSpeed = 10;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    public Vector3 Min;
    public Vector3 Max;

    public bool enabledMovement = false;

    private float timer;

    private bool inProgress = false;
    private void Start()
    {
        _originalPosition = Mover.Rigidbody.position;
        _originalRotation = Mover.Rigidbody.rotation;

        Mover.MoverController = this;

        Min.x = _originalPosition.x;
        Min.y = _originalPosition.y;
        Min.z = _originalPosition.z;

        Max.x = 1000;
        Max.y = 1000;
        Max.z = 1000;
        timer = 0;

    }
  
    public void UpdateMovement(out Vector3 goalPosition, out Quaternion goalRotation, float deltaTime)
    {
        if (enabledMovement || inProgress)
        {
            inProgress = true;
            timer += Time.deltaTime;
            goalPosition = (_originalPosition + (TranslationAxis.normalized * Mathf.Sin(timer * TranslationSpeed) * TranslationPeriod));
            goalPosition.y = Mathf.Clamp(goalPosition.y, _originalPosition.y, Max.y);
            Quaternion targetRotForOscillation = Quaternion.Euler(OscillationAxis.normalized * (Mathf.Sin(timer * OscillationSpeed) * OscillationPeriod)) * _originalRotation;
            goalRotation = Quaternion.Euler(RotationAxis * RotSpeed * Time.time) * targetRotForOscillation;
        }
        else
        {
            goalPosition = Mover.Rigidbody.position;
            goalRotation = Mover.Rigidbody.rotation;
        }
    }
}
