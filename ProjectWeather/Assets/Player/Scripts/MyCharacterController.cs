using UnityEngine;
using KinematicCharacterController;


namespace Assets.Player.Scripts
{
    public struct PlayerCharacterInputs
    {
        public float MoveAxisForward;
        public float MoveAxisRight;
        public bool JumpDown;
        public bool SprintHoldDown;
    }

    public class MyCharacterController : MonoBehaviour, ICharacterController
    {
        public KinematicCharacterMotor Motor;

        [Header("Stable Movement")]
        public float MaxStableMoveSpeed = 10f;
        public float StableMovementSharpness = 15;
        public float OrientationSharpness = 10;
        public float TurnSpeed = 10f;

        [Header("Air Movement")]
        public float MaxAirMoveSpeed = 10f;
        public float AirAccelerationSpeed = 5f;
        public float Drag = 0.1f;

        [Header("Sprinting")]
        public bool AllowSprint = true;
        public float SprintSpeedBoost = 1.5f;

        [Header("Jumping")]
        public bool AllowJumpingWhenSliding = false;
        public bool AllowDoubleJump = true;
        public float JumpSpeed = 10f;
        public float JumpPreGroundingGraceTime = 0f;
        //public float JumpPostGroundingGraceTime = 0f;     // May be useful at some point

        [Header("Misc")]
        public Vector3 Gravity = new Vector3(0, -30f, 0);
        public Transform MeshRoot;

        // Private variables
        private PlayerAbility _ability;
        private Vector3 _moveInputVector;
        private Vector3 _currentVelocity = Vector3.zero; // used for getter function
        private bool _jumpRequested = false;
        private bool _jumpConsumed = false;
        private bool _jumpedThisFrame = false;
        private float _timeSinceJumpRequested = Mathf.Infinity;
        private float _timeSinceLastAbleToJump = 0f;
        private bool _doubleJumpConsumed = false;
        private bool _impulseConsumed = false;
        private Vector3 _internalVelocityAdd = Vector3.zero;
        private float _maxMoveSpeed = 10f;
        private bool _sprintActivated = false;

        // Animation and Cinemachine variables
        private float _moveSpeed;
        private float _turnSpeedMultiplier;
        private Vector3 _lookDirection;           // Character look direction
        private Camera _mainCamera;
        private Quaternion _currentRotation;


        private void Awake()
        {
            _ability = GetComponent<PlayerAbility>();
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _maxMoveSpeed = MaxStableMoveSpeed;
            _turnSpeedMultiplier = 10f;

            // Assign to motor
            Motor.CharacterController = this;
        }

        /// <summary>
        /// This is called every frame by MyPlayer in order to tell the character what its inputs are
        /// </summary>
        public void SetInputs(ref PlayerCharacterInputs inputs)
        {
            // Clamp input
            Vector3 moveInputVector =
                Vector3.ClampMagnitude(new Vector3(inputs.MoveAxisRight, 0f, inputs.MoveAxisForward), 1f);

            // Set the character movement speed. Used for the animations
            _moveSpeed = Mathf.Abs(inputs.MoveAxisRight) + Mathf.Abs(inputs.MoveAxisForward);
            _moveSpeed = Mathf.Clamp(_moveSpeed, 0f, 1f);


            UpdateTargetDirection(ref inputs);

            if (moveInputVector != Vector3.zero && _lookDirection.magnitude > 0.1f)
            {
                var freeRotation = Quaternion.LookRotation(_lookDirection, transform.up);
                var differenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (differenceRotation < 0 || differenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                _currentRotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), TurnSpeed * _turnSpeedMultiplier * Time.deltaTime);
            }

            // Move and look inputs
            _moveInputVector = _lookDirection; // cameraPlanarRotation * moveInputVector;

            // Jumping input
            if (inputs.JumpDown)
            {
                _timeSinceJumpRequested = 0f;
                _jumpRequested = true;
            }

            // Impulse/Wind Ability
            if (_ability.AllowWindAbility)
            {
                HandleWindAbility();
            }

            // Sprint input
            if (AllowSprint)
            {
                if (!_sprintActivated && inputs.SprintHoldDown && _moveInputVector.magnitude > 0.1)
                {
                    _maxMoveSpeed += SprintSpeedBoost;
                    _sprintActivated = true;

                } else if (!inputs.SprintHoldDown)
                {
                    _maxMoveSpeed = MaxStableMoveSpeed;
                    _sprintActivated = false;
                }
            }
        }


        public virtual void UpdateTargetDirection(ref PlayerCharacterInputs inputs)
        {
            var forward = _mainCamera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            // Get the right-facing direction of the referenceTransform
            var right = _mainCamera.transform.TransformDirection(Vector3.right);

            // Determine the direction the player will face based on input and the referenceTransform's right and forward directions
            _lookDirection = (inputs.MoveAxisForward * forward + inputs.MoveAxisRight * right).normalized;
        }


        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is called before the character begins its movement update
        /// </summary>
        public void BeforeCharacterUpdate(float deltaTime)
        {
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is where you tell your character what its rotation should be right now.
        /// This is the ONLY place where you should set the character's rotation
        /// </summary>
        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
        {
            currentRotation = _currentRotation;
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is where you tell your character what its velocity should be right now.
        /// This is the ONLY place where you can set the character's velocity
        /// </summary>
        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
        {
            Vector3 targetMovementVelocity = Vector3.zero;
            if (Motor.GroundingStatus.IsStableOnGround)
            {
                // Reorient velocity on slope
                currentVelocity =
                    Motor.GetDirectionTangentToSurface(currentVelocity, Motor.GroundingStatus.GroundNormal) *
                    currentVelocity.magnitude;

                // Calculate target velocity
                Vector3 inputRight = Vector3.Cross(_moveInputVector, Motor.CharacterUp);
                Vector3 reorientedInput = Vector3.Cross(Motor.GroundingStatus.GroundNormal, inputRight).normalized *
                                          _moveInputVector.magnitude;
                targetMovementVelocity = reorientedInput * _maxMoveSpeed;

                // Smooth movement Velocity
                currentVelocity = Vector3.Lerp(currentVelocity, targetMovementVelocity,
                    1 - Mathf.Exp(-StableMovementSharpness * deltaTime));
            }
            else
            {
                // Add move input
                if (_moveInputVector.sqrMagnitude > 0f)
                {
                    targetMovementVelocity = _moveInputVector * MaxAirMoveSpeed;

                    // Prevent climbing on un-stable slopes with air movement
                    if (Motor.GroundingStatus.FoundAnyGround)
                    {
                        Vector3 perpenticularObstructionNormal = Vector3
                            .Cross(Vector3.Cross(Motor.CharacterUp, Motor.GroundingStatus.GroundNormal),
                                Motor.CharacterUp)
                            .normalized;
                        targetMovementVelocity =
                            Vector3.ProjectOnPlane(targetMovementVelocity, perpenticularObstructionNormal);
                    }

                    Vector3 velocityDiff = Vector3.ProjectOnPlane(targetMovementVelocity - currentVelocity, Gravity);
                    currentVelocity += velocityDiff * AirAccelerationSpeed * deltaTime;
                }

                // Gravity
                currentVelocity += Gravity * deltaTime;

                // Drag
                currentVelocity *= (1f / (1f + (Drag * deltaTime)));
            }

            HandleJumping(ref currentVelocity, deltaTime);

            // Take into account additive velocity
            if (_internalVelocityAdd.sqrMagnitude > 0f)
            {
                currentVelocity += _internalVelocityAdd;
                _internalVelocityAdd = Vector3.zero;
            }

            // Update internal private current velocity
            _currentVelocity = currentVelocity;
        }


        private void HandleJumping(ref Vector3 currentVelocity, float deltaTime)
        {
            _jumpedThisFrame = false;
            _timeSinceJumpRequested += deltaTime;
            if (_jumpRequested)
            {
                // Handle double jump
                if (AllowDoubleJump)
                {
                    if (_jumpConsumed && !_doubleJumpConsumed)
                    {
                        Motor.ForceUnground(0.1f);

                        // Add to the return velocity and reset jump state
                        currentVelocity += (Motor.CharacterUp * JumpSpeed) -
                                           Vector3.Project(currentVelocity, Motor.CharacterUp);
                        _jumpRequested = false;
                        _doubleJumpConsumed = true;
                        _jumpedThisFrame = true;
                    }
                }

                // See if we actually are allowed to jump
                if (!_jumpConsumed)
                {
                    // Calculate jump direction before ungrounding
                    Vector3 jumpDirection = Motor.GroundingStatus.GroundNormal;

                    // Makes the character skip ground probing/snapping on its next update.
                    // If this line weren't here, the character would remain snapped to the ground when trying to jump. Try commenting this line out and see.
                    Motor.ForceUnground(0.1f);

                    // Add to the return velocity and reset jump state
                    currentVelocity += (jumpDirection * JumpSpeed) -
                                       Vector3.Project(currentVelocity, Motor.CharacterUp);
                    _jumpRequested = false;
                    _jumpConsumed = true;
                    _jumpedThisFrame = true;
                }
            }
        }

        /// <summary>
        /// (Called by AfterCharacterUpdate)
        /// This is called after the character has finished its movement update
        /// and updates the jump-related global variables
        /// </summary>
        private void HandleJumpValues(float deltaTime)
        {
            // Handle jumping pre-ground grace period
            if (_jumpRequested && _timeSinceJumpRequested > JumpPreGroundingGraceTime)
            {
                _jumpRequested = false;
            }

            if (AllowJumpingWhenSliding ? Motor.GroundingStatus.FoundAnyGround : Motor.GroundingStatus.IsStableOnGround)
            {
                // If we're on a ground surface, reset jumping values
                if (!_jumpedThisFrame)
                {
                    _doubleJumpConsumed = false;
                    _jumpConsumed = false;
                }

                _timeSinceLastAbleToJump = 0f;
            }
            else
            {
                // Keep track of time since we were last able to jump (for grace period)
                _timeSinceLastAbleToJump += deltaTime;
            }
        }


        /// <summary>
        /// (Called by SetInputs every frame)
        /// This is the Character's 'Wind' Ability. Only usable when the character
        /// is on the ground. i.e. can't jump, then use it
        /// </summary>
        private void HandleWindAbility()
        {
            if (_ability.IsAbilityBeingPressed(Weather.Wind) && !_impulseConsumed)
            {
                _impulseConsumed = true;
                Motor.ForceUnground(0.1f);
                AddVelocity((_moveInputVector.normalized + Motor.CharacterUp) * _ability.ImpulseMagnitude);
            }
        }

        private void HandleImpulseValues()
        {
            if (Motor.GroundingStatus.IsStableOnGround)
            {
                _impulseConsumed = false;
            }
        }

        /// <summary>
        /// (Called by KinematicCharacterMotor during its update cycle)
        /// This is called after the character has finished its movement update
        /// </summary>
        public void AfterCharacterUpdate(float deltaTime)
        {
            // Handle jump-related values
            HandleJumpValues(deltaTime);

            HandleImpulseValues();
        }

        public bool IsColliderValidForCollisions(Collider coll)
        {
            return true;
        }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
        }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            ref HitStabilityReport hitStabilityReport)
        {
        }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint,
            Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
        {
        }

        public void PostGroundingUpdate(float deltaTime)
        {
        }

        public void AddVelocity(Vector3 velocity)
        {
            _internalVelocityAdd += velocity;
        }

        public void OnDiscreteCollisionDetected(Collider hitCollider)
        {
        }

        public bool IsPlayerOnGround()
        {
            return Motor.GroundingStatus.IsStableOnGround;
        }

        public float GetPlayerCurrentVelocity()
        {
            return _currentVelocity.magnitude;
        }

        public float GetMoveSpeed()
        {
            return _moveSpeed;
        }

        public bool DidPlayerJump()
        {
            return _jumpConsumed;
        }

        public bool DidPlayerDoubleJump()
        {
            return _doubleJumpConsumed;
        }

        public bool IsPlayerSprinting()
        {
            return _sprintActivated;
        }

        public void LaunchPlayer(Vector3 direction, float magnitude)
        {
            Motor.ForceUnground(0.1f);
            AddVelocity(direction * magnitude);
        }
    }
}
