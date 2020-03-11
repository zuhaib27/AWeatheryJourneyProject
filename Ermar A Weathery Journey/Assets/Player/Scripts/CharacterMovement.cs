using System;
using UnityEngine;

namespace Assets.Player.Scripts
{
  //[AddComponentMenu("")] // Don't display in add component menu
  public class CharacterMovement : MonoBehaviour
  {
    [Header("Movement Settings")]
    public bool UseCharacterForward = false;
    public float TurnSpeed = 10f;
    public float MoveSpeed = 8f;

    [Header("Jump Settings")]
    public float JumpForce = 5f;

    private CharacterController _characterController;
    private float _turnSpeedMultiplier;
    private Vector3 _moveDirection;
    private Vector2 _input;
    private Quaternion _freeRotation;
    private Camera _mainCamera;

    private float _verticalVelocity;         // vertical y velocity


    private void Awake()
    {
      _characterController = GetComponent<CharacterController>();
      _mainCamera = Camera.main;
    }

    // Use this for initialization
    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    private void Update()
    {
      _input.x = Input.GetAxis("Horizontal");
      _input.y = Input.GetAxis("Vertical");
    }

    
    private void FixedUpdate()
    {
      // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
      UpdateCharacterDirection();

      // Rotate the character
      UpdateCharacterRotation();

      // Handle jumping
      HandleJump();

      // Move the controller
      _characterController.Move(_moveDirection * Time.fixedDeltaTime);
    }





    public virtual void UpdateCharacterDirection()
    {
      if (!UseCharacterForward)
      {
        _turnSpeedMultiplier = 1f;
        var forward = _mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        // Get the right-facing direction of the referenceTransform
        var right = _mainCamera.transform.TransformDirection(Vector3.right);

        // Determine the direction the player will face based on input and the referenceTransform's right and forward directions
        _moveDirection = _input.x * right + _input.y * forward;
      }
      else
      {
        _turnSpeedMultiplier = 0.2f;
        var forward = transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        // Get the right-facing direction of the referenceTransform
        var right = transform.TransformDirection(Vector3.right);
        _moveDirection = _input.x * right + Mathf.Abs(_input.y) * forward;
      }

      //_moveDirection = transform.TransformDirection(_moveDirection);
      _moveDirection = _moveDirection.normalized;
      _moveDirection *= MoveSpeed;
    }


    private void UpdateCharacterRotation()
    {
      if (_input == Vector2.zero || _moveDirection.magnitude <= 0.1f)
      {
        return;
      }

      var lookDirection = _moveDirection.normalized;
      _freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
      var differenceRotation = _freeRotation.eulerAngles.y - transform.eulerAngles.y;
      var eulerY = transform.eulerAngles.y;

      if (differenceRotation < 0 || differenceRotation > 0) eulerY = _freeRotation.eulerAngles.y;
      var euler = new Vector3(0, eulerY, 0);

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler),
        TurnSpeed * _turnSpeedMultiplier * Time.fixedDeltaTime);
    }

    private void HandleJump()
    {
      _verticalVelocity += Physics.gravity.y * Time.fixedDeltaTime;

      if (_characterController.isGrounded && Input.GetButton("Jump"))
      {
        _verticalVelocity = JumpForce;
      }

      _moveDirection.y = _verticalVelocity;
    }
  }
}