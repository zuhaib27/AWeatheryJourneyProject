using UnityEngine;
using AWeatheryJourney;

namespace Assets.Player.Scripts
{
    public class PlayerInputs : MonoBehaviour
    {
        public PlayerSettings Settings { get; set; }


        public MyCharacterController Character;

        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

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

            HandleCharacterInput();
        }

        private void HandleCharacterInput()
        {
            var characterInputs = new PlayerCharacterInputs
            {
                // Build the CharacterInputs struct
                MoveAxisForward = ButtonMappings.GetAxisInput(VerticalInput),
                MoveAxisRight = ButtonMappings.GetAxisInput(HorizontalInput),
                JumpDown = ButtonMappings.GetButtonDown(Button.Jump),
                //SprintHoldDown = Input.GetButton(SprintInput) || Input.GetKey(KeyCode.JoystickButton2),
            };

            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
        }
    }
}
