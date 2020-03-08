using UnityEngine;

namespace Assets.Player.Scripts
{
    public class CharacterAnimator : MonoBehaviour
    {
        //[Header("Animation Parameters")]

        
        // Private variables
        private Animator _animator;
        private MyCharacterController _characterController;
        private PlayerAbility _playerAbility;
        private float _moveSpeed = 0f;
        private float _currentVelocity;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<MyCharacterController>();
            _playerAbility = GetComponent<PlayerAbility>();
        }

        // Update is called once per frame
        private void Update()
        {
            _moveSpeed = _characterController.GetMoveSpeed();
            _moveSpeed = Mathf.SmoothDamp(_animator.GetFloat("MoveSpeed"), _moveSpeed, ref _currentVelocity, 0.1f);

            HandleAnimation();
        }

        private void HandleAnimation()
        {
            _animator.SetFloat("MoveSpeed", _moveSpeed);
            _animator.SetBool("IsSprinting", _characterController.IsPlayerSprinting());
            _animator.SetBool("OnGround", _characterController.IsPlayerOnGround());
            _animator.SetBool("Jump", _characterController.DidPlayerJump());
            _animator.SetBool("DoubleJump", _characterController.DidPlayerDoubleJump());
            _animator.SetBool("WindAbility", _playerAbility.IsAbilityBeingPressed(Weather.Wind));
            _animator.SetBool("FrostAbility", _playerAbility.IsAbilityBeingPressed(Weather.Frost));
            _animator.SetBool("SunAbility", _playerAbility.IsAbilityBeingPressed(Weather.Sun));
            _animator.SetBool("RainAbility", _playerAbility.IsAbilityBeingPressed(Weather.Rain));
        }
    }
}
