/**
 * NOTE: I've commented out this script because Cinemachine is not used, but don't delete
 * this script please! :)
 */

// using UnityEngine;

// namespace Assets.Player.Scripts
// {
//   public class CharMovement : MonoBehaviour
//   {
//     public float MoveSpeed = 5;

//     private const float RotationDamping = 0.5f;
//     private Vector3 _input;


//     private void Start()
//     {
//       Cursor.lockState = CursorLockMode.Locked;
//     }


//     private void Update()
//     {
//       _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

//       if (_input.magnitude <= 0)
//       {
//         return;
//       }

//       var fwd = transform.position - Camera.main.transform.position;
//       fwd.y = 0;
//       fwd = fwd.normalized;
//       if (fwd.magnitude <= 0.001f)
//       {
//         return;
//       }

//       var inputFrame = Quaternion.LookRotation(fwd, Vector3.up);
//       _input = inputFrame * _input;
//       if (_input.magnitude <= 0.001f)
//       {
//         return;
//       }

//       // Move the character
//       transform.position += _input * MoveSpeed * Time.deltaTime;

//       // Rotate the character
//       var t = Cinemachine.Utility.Damper.Damp(1, RotationDamping, Time.deltaTime);
//       var newRotation = Quaternion.LookRotation(_input.normalized, Vector3.up);
//       transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, t);
//     }
//   }
// }
