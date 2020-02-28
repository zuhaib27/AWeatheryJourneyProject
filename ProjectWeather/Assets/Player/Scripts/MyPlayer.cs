using UnityEngine;

namespace Assets.Player.Scripts
{
  public class MyPlayer : MonoBehaviour
  {
    public PlayerSettings Settings { get; set; }

    public CharacterCamera OrbitCamera;
    public Transform CameraFollowPoint;
    public MyCharacterController Character;

    public float LookSensitivity = 1.0f;

    private const string MouseXInput = "Mouse X";
    private const string MouseYInput = "Mouse Y";
    private const string MouseScrollInput = "Mouse ScrollWheel";
    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";
    private const string JumpInput = "Jump";

    private void Start()
    {
      Cursor.lockState = CursorLockMode.Locked;

      // Tell camera to follow transform
      OrbitCamera.SetFollowTransform(CameraFollowPoint);

      // Ignore the character's collider(s) for camera obstruction checks
      OrbitCamera.IgnoredColliders.Clear();
      OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());

      // Get settings for player
      if (SettingsManager.Instance)
      {
        Settings = SettingsManager.Instance.Settings.playerSettings;
      }
      else
      {
        Debug.LogWarning("No SettingsManager found in scene");
      }
    }

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        Cursor.lockState = CursorLockMode.Locked;
      }

      HandleCameraInput();
      HandleCharacterInput();
    }

    private void HandleCameraInput()
    {
      // Create the look input vector for the camera
      float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput) * (Settings.invertCameraY ? -1 : 1) * LookSensitivity;
      float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput) * LookSensitivity;
      Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

      // Prevent moving the camera while the cursor isn't locked
      if (Cursor.lockState != CursorLockMode.Locked)
      {
        lookInputVector = Vector3.zero;
      }

      // Input for zooming the camera (disabled in WebGL because it can cause problems)
      float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

      // Apply inputs to the camera
      OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

      // Handle toggling zoom level
      if (Input.GetMouseButtonDown(1))
      {
        OrbitCamera.TargetDistance = (OrbitCamera.TargetDistance == 0f) ? OrbitCamera.DefaultDistance : 0f;
      }
    }

    private void HandleCharacterInput()
    {
      var characterInputs = new PlayerCharacterInputs
      {

        // Build the CharacterInputs struct
        MoveAxisForward = Input.GetAxisRaw(VerticalInput),
        MoveAxisRight = Input.GetAxisRaw(HorizontalInput),
        CameraRotation = OrbitCamera.Transform.rotation,
        JumpDown = Input.GetButtonDown(JumpInput) || Input.GetKeyDown(KeyCode.JoystickButton0),
        ImpulseDown = Input.GetKeyDown(KeyCode.JoystickButton3)
      };

      // Apply inputs to character
      Character.SetInputs(ref characterInputs);
    }
  }
}
