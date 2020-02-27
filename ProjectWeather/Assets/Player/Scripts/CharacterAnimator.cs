using UnityEngine;

namespace Assets.Player.Scripts
{
    public class CharacterAnimator : MonoBehaviour
    {
        [Header("Animation Parameters")]
        //public Animator CharacterAnimator;
        public float ForwardAxisSharpness = 10;
        public float TurnAxisSharpness = 5;
        
        // Private variables
        private Animator _animator;
        private MyCharacterController _characterController;
        private float _forwardAxisSpeed = 0f;
        private float _horizontalAxisSpeed = 0f;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<MyCharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            Vector3 moveInputVector = _characterController.GetMovementInputVector();
            var targetForwardAxis = moveInputVector.z;
            var targetHorizontalAxis = moveInputVector.x;

            _forwardAxisSpeed = Mathf.Lerp(_forwardAxisSpeed, targetForwardAxis, 1f - Mathf.Exp(-ForwardAxisSharpness * Time.deltaTime));
            _horizontalAxisSpeed = Mathf.Lerp(_horizontalAxisSpeed, targetHorizontalAxis, 1f - Mathf.Exp(-TurnAxisSharpness * Time.deltaTime));

            HandleAnimation();
        }

        private void HandleAnimation()
        {
            _animator.SetFloat("ForwardSpeed", _forwardAxisSpeed);
            _animator.SetFloat("HorizontalSpeed", _horizontalAxisSpeed);
            _animator.SetBool("OnGround", _characterController.IsPlayerOnGround());
            _animator.SetBool("Jump", _characterController.DidPlayerJump());
            _animator.SetBool("DoubleJump", _characterController.DidPlayerDoubleJump());
            _animator.SetBool("WindAbility", _characterController.DidPlayerActivateWind());
        }
    }
}
