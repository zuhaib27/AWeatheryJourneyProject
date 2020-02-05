using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  CharacterController controller;

  public float turnSpeed = 20f;
  public float walkSpeed = 12f;
  public float jumpHeight = 1.2f;
  public float gravityModifier = 2.5f;

  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;

  public Transform camera;

    Vector3 movement;

  float gravity = -9.81f;
  Vector3 curVelocity;
  
  void Start()
  {
    controller = GetComponent<CharacterController>();
  }
  
  void Update()
  {
    bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    // Move in direction, relative to the camera
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    movement = camera.right * x + camera.forward * z;
    movement.y = 0;
    controller.Move(movement * walkSpeed * Time.deltaTime);

    // Rotate character towards new direction
    Vector3 desiredForward = Vector3.RotateTowards(transform.forward, movement, turnSpeed * Time.deltaTime, 0f);
    Quaternion rotation = Quaternion.LookRotation(desiredForward, Vector3.up);
    transform.rotation = rotation;

    // Gravity check
    if (isGrounded && curVelocity.y < 0)
    {
      curVelocity.y = -2f;
    }

    // Jumping
    if (Input.GetButtonDown("Jump") && isGrounded)
    {
      curVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
    else if (Input.GetButtonDown("Jump"))
    {
      Debug.Log("Not Grounded!");
    }

    // Y-movement
    curVelocity.y += gravity * Time.deltaTime;
    controller.Move(curVelocity * Time.deltaTime);

        //Vector3 desiredDirection = new Vector3(hor, 0, ver);
        //Quaternion desiredRotation = Quaternion.LookRotation(new Vector3(hor, 0, ver));
        //Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, turnSpeed * Time.deltaTime);
        //Vector3 newDirection = Vector3.RotateTowards(transform.forward, transform.forward + desiredDirection, turnSpeed * Time.deltaTime, 0f);
        //Quaternion newRotation = Quaternion.LookRotation(newDirection);

        //rb.MovePosition(transform.position + newDirection * walkSpeed * Time.deltaTime);
        //rb.MoveRotation(newRotation);
    }

    public Vector3 GetPlayerSpeed() { return movement * walkSpeed; }
}
