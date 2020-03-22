using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AWeatheryJourney
{
    public enum Button
    {
        None,
        WindActivate,
        RainActivate,
        SunActivate,
        FrostActivate,
        AbilityUse,
        Jump,
    }

    public static class ButtonMappings
    {
        // Regular movement inputs
        private const string _vertical = "Vertical";
        private const string _horizontal = "Horizontal";

        private const string _windActivate0 = "DPad Vertical";
        private const string _rainActivate0 = "DPad Vertical";
        private const string _sunActivate0 = "DPad Horizontal";
        private const string _frostActivate0 = "DPad Horizontal";
        private const KeyCode _windActivate1 = KeyCode.Alpha1;
        private const KeyCode _rainActivate1 = KeyCode.Alpha2;
        private const KeyCode _sunActivate1 = KeyCode.Alpha3;
        private const KeyCode _frostActivate1 = KeyCode.Alpha4;

        private const KeyCode _abilityUse0 = KeyCode.JoystickButton1;
        private const KeyCode _abilityUse1 = KeyCode.F;

        private const KeyCode _jump0 = KeyCode.JoystickButton0;
        private const KeyCode _jump1 = KeyCode.Space;


        public static float GetAxisInput(string axis)
        {
            float axisInput = 0f;

            if (axis == _vertical)
            {
                axisInput = Input.GetAxisRaw(_vertical);
            }
            else if (axis == _horizontal)
            {
                axisInput = Input.GetAxisRaw(_horizontal);
            }
            else
            {
                // TODO: Add proper error?
                Debug.LogError("Must provide proper input axis string: 'Horizontal' or 'Vertical'");
            }

            return axisInput;
        }


        public static bool GetButtonDown(Button button)
        {
            bool result = false;

            switch(button)
            {
                case Button.WindActivate:
                    result = Input.GetAxisRaw(_windActivate0) > 0 || Input.GetKeyDown(_windActivate1);
                    break;
                case Button.RainActivate:
                    result = Input.GetAxisRaw(_rainActivate0) < 0 || Input.GetKeyDown(_rainActivate1);
                    break;
                case Button.SunActivate:
                    result = Input.GetAxisRaw(_sunActivate0) < 0 || Input.GetKeyDown(_sunActivate1);
                    break;
                case Button.FrostActivate:
                    result = Input.GetAxisRaw(_frostActivate0) > 0 || Input.GetKeyDown(_frostActivate1);
                    break;
                case Button.AbilityUse:
                    result = Input.GetKeyDown(_abilityUse0) || Input.GetKeyDown(_abilityUse1);
                    break;
                case Button.Jump:
                    result = Input.GetKeyDown(_jump0) || Input.GetKeyDown(_jump1);
                    break;
                default:
                    result =  false;
                    break;
            }

            return result;
        }

        public static bool GetButton(Button button)
        {
            bool result = false;

            switch (button)
            {
                case Button.WindActivate:
                    result = Input.GetAxisRaw(_windActivate0) > 0 || Input.GetKey(_windActivate1);
                    break;
                case Button.RainActivate:
                    result = Input.GetAxisRaw(_rainActivate0) < 0 || Input.GetKey(_rainActivate1);
                    break;
                case Button.SunActivate:
                    result = Input.GetAxisRaw(_sunActivate0) < 0 || Input.GetKey(_sunActivate1);
                    break;
                case Button.FrostActivate:
                    result = Input.GetAxisRaw(_frostActivate0) > 0 || Input.GetKey(_frostActivate1);
                    break;
                case Button.AbilityUse:
                    result = Input.GetKey(_abilityUse0) || Input.GetKey(_abilityUse1);
                    break;
                case Button.Jump:
                    result = Input.GetKey(_jump0) || Input.GetKey(_jump1);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

        public static bool GetButtonUp(Button button)
        {
            bool result = false;

            switch (button)
            {
                case Button.WindActivate:
                    result = Input.GetAxisRaw(_windActivate0) > 0 || Input.GetKeyUp(_windActivate1);
                    break;
                case Button.RainActivate:
                    result = Input.GetAxisRaw(_rainActivate0) < 0 || Input.GetKeyUp(_rainActivate1);
                    break;
                case Button.SunActivate:
                    result = Input.GetAxisRaw(_sunActivate0) < 0 || Input.GetKeyUp(_sunActivate1);
                    break;
                case Button.FrostActivate:
                    result = Input.GetAxisRaw(_frostActivate0) > 0 || Input.GetKeyUp(_frostActivate1);
                    break;
                case Button.AbilityUse:
                    result = Input.GetKeyUp(_abilityUse0) || Input.GetKeyUp(_abilityUse1);
                    break;
                case Button.Jump:
                    result = Input.GetKeyUp(_jump0) || Input.GetKeyUp(_jump1);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }
    }
}
