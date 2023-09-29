using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Drawing;
using System.Runtime.CompilerServices;
public class CharStateMachine : MonoBehaviour
{
    //CLEAN UP CODE

    [Header("Refrences")]
    #region Refrences

    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;

    [SerializeField] Transform _playerObj;
    public Transform PlayerObj
    {
        get
        {
            return _playerObj;
        }
    }

    [SerializeField] private Transform _orientation;
    public Transform Orientation
    {
        get
        {
            return _orientation;
        }
    }

    [SerializeField] Rigidbody _rb;
    public Rigidbody Rb
    {
        get
        {
            return _rb;
        }
    }

    CharStateFactory _states;
    CharBaseState _currentState;
    public CharBaseState CurrentState
    {
        get
        {
            return _currentState;
        }
        set
        {
            _currentState = value;
        }
    }

    #endregion

    [Header("Movement")]
    #region Movement

    [SerializeField] Vector2 _currentMovementInput;
    public Vector2 CurrentMovementInput
    {
        get
        {
            return _currentMovementInput;
        }
    }

    [SerializeField] Vector3 _currentMovement;
    public Vector3 CurrentMovement
    {
        get
        {
            return _currentMovement;
        }
        set
        {
            _currentMovement = value;
        }
    }

    [SerializeField] Vector3 _movement;
    public Vector3 Movement
    {
        get
        {
            return _movement;
        }
        set
        {
            _movement = value;
        }
    }

    [SerializeField] float _moveForce;
    public float MoveForce
    {
        get
        {
            return _moveForce;
        }
        set
        {
            _moveForce = value;
        }
    }

    [SerializeField] float _desiredMoveForce;
    public float DesiredMoveForce
    {
        get
        {
            return _desiredMoveForce;
        }
        set
        {
            _desiredMoveForce = value;
        }
    }

    [SerializeField] float _lastDesiredMoveForce;
    public float LastDesiredMoveForce
    {
        get
        {
            return _lastDesiredMoveForce;
        }
        set
        {
            _lastDesiredMoveForce = value;
        }
    }

    #endregion

    [Header("Jumping")]
    #region Jumping

    [SerializeField] float _jumpForce;
    public float JumpForce
    {
        get
        {
            return _jumpForce;
        }
    }

    [SerializeField] float _maxJumpTime;
    public float MaxJumpTime
    {
        get
        {
            return _maxJumpTime;
        }
    }

    [SerializeField] float _isJumpTime;
    public float IsJumpTime
    {
        get
        {
            return _isJumpTime;
        }
        set
        {
            _isJumpTime = value;
        }
    }

    #endregion

    [Header("Sliding")]
    #region Sliding

    [SerializeField] float _slideForce;
    public float SlideForce
    {
        get
        {
            return _slideForce;
        }
    }

    [SerializeField] bool _isSliding;
    public bool IsSliding
    {
        get
        {
            return _isSliding;
        }
        set
        {
            _isSliding = value;
        }
    }

    #endregion

    [Header("WallRunning")]
    #region WallRunning

    [SerializeField] bool _wallRight;
    public bool WallRight
    {
        get
        {
            return _wallRight;
        }
        set
        {
            _wallRight = value;
        }
    }

    [SerializeField] RaycastHit rightWallhit;

    [SerializeField] bool _wallLeft;
    public bool WallLeft
    {
        get
        {
            return _wallLeft;
        }
        set
        {
            _wallLeft = value;
        }
    }

    [SerializeField] RaycastHit leftWallhit;

    [SerializeField] LayerMask _wallLayer;

    [SerializeField] float _wallCheckDistance;
    public float WallCheckDistance
    {
        get
        {
            return _wallCheckDistance;
        }
    }


    #endregion

    [Header("Groundcheck")]
    #region GroundCheck

    [SerializeField] bool _isGrounded;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        set
        {
            _isGrounded = value;
        }
    }

    [SerializeField] LayerMask _groundLayer;

    [SerializeField] float _groundDrag;
    public float GroundDrag
    {
        get
        {
            return _groundDrag;
        }
    }

    [SerializeField] float sphereRadius;
    [SerializeField] float sphereOffset;

    #endregion

    [Header("SlopeCheck")]
    #region SlopeCheck

    [SerializeField] bool _isSloped;
    public bool IsSloped
    {
        get
        {
            return _isSloped;
        }
        set
        {
            _isSloped = value;
        }
    }
    [SerializeField] bool _isExitingSlope;
    public bool IsExitingSlope
    {
        get
        {
            return _isExitingSlope;
        }
        set
        {
            _isExitingSlope = value;
        }
    }

    RaycastHit _slopeHit;

    [SerializeField] float _maxSlopeAngle;

    [SerializeField] float _playerHeight;

    #endregion

    [Header("Inputs")]
    #region Inputs

    [SerializeField] bool _isMove;
    public bool IsMove
    {
        get
        {
            return _isMove;
        }
    }

    [SerializeField] bool _isSlide;
    public bool IsSlide
    {
        get
        {
            return _isSlide;
        }
    }

    [SerializeField] bool _isJump;
    public bool IsJump
    {
        get
        {
            return _isJump;
        }
    }

    #endregion

    [Header("Speeds")]
    #region Speeds

    [SerializeField] float _speedIncreaseMultiplier;
    public float SpeedIncreaseMultiplier
    {
        get
        {
            return _speedIncreaseMultiplier;
        }
    }

    [SerializeField] float _slopeSpeedIncreaseMultiplier;
    public float SlopeSpeedIncreaseMultiplier
    {
        get
        {
            return _slopeSpeedIncreaseMultiplier;
        }
    }

    [SerializeField] float _moveSpeed;
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }

    [SerializeField] float _slideSpeed;
    public float SlideSpeed
    {
        get
        {
            return _slideSpeed;
        }
    }

    [SerializeField] float _slopeSlideSpeed;
    public float SlopeSlideSpeed
    {
        get
        {
            return _slopeSlideSpeed;
        }
    }

    [SerializeField] float _wallRunSpeed;
    public float WallRunSpeed
    {
        get
        {
            return _wallRunSpeed;
        }
    }

    [SerializeField] float _moveMultiplier;
    public float MoveMultiplier
    {
        get
        {
            return _moveMultiplier;
        }
        set
        {
            _moveMultiplier = value;
        }
    }


    #endregion


    private void Awake()
    {
        Application.targetFrameRate = 60;
        playerInput.actions.FindAction("Move").started += OnMovement;
        playerInput.actions.FindAction("Move").performed += OnMovement;
        playerInput.actions.FindAction("Move").canceled += OnMovement;

        playerInput.actions.FindAction("Slide").started += OnSlide;
        playerInput.actions.FindAction("Slide").performed += OnSlide;
        playerInput.actions.FindAction("Slide").canceled += OnSlide;

        playerInput.actions.FindAction("Jump").started += OnJump;
        playerInput.actions.FindAction("Jump").performed += OnJump;
        playerInput.actions.FindAction("Jump").canceled += OnJump;

        _states = new CharStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
        IsGrounded = true;
    }

    #region MonoBehaviours

    [SerializeField] TMP_Text text;

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
            Debug.Log(CurrentState);

        CurrentMovement = Orientation.forward * CurrentMovementInput.y + Orientation.right * CurrentMovementInput.x;

        _currentState.UpdateStates();

        IsGrounded = CheckGrounded();
        IsSloped = CheckSloped();
        CheckForWall();

        SpeedControl();

        // DO THIS IN GROUNDED STATE NOT IN STATE MACHINE
        if (IsGrounded || IsSloped)
        {
            Rb.drag = GroundDrag;
        }
        else if (!IsSloped && !IsGrounded)
        {
            Rb.drag = 0;
        }

        if (Mathf.Abs(DesiredMoveForce - LastDesiredMoveForce) > 4f && MoveForce != 0)
        {
            StopAllCoroutines();
            StartCoroutine(SmoovMoov());
        }
        else
        {
            MoveForce = DesiredMoveForce;
        }

        LastDesiredMoveForce = DesiredMoveForce;
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
    }

    private void Lateupdate()
    {
        _currentState.LateUpdateStates();
    }

    private void OnTriggerEnter(Collider other)
    {
        // _currentState.OnTriggerEnterStates(other);
    }

    private void OnTriggerExit(Collider other)
    {
        // _currentState.OnTriggerExitStates(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        // _currentState.OnCollisionEnterStates(other);
    }

    private void OnCollisionExit(Collision other)
    {
        // _currentState.OnCollisionExitStates(other);
    }

    #endregion

    #region InputVoids

    void OnMovement(InputAction.CallbackContext context)
    {
        _currentMovementInput = context.ReadValue<Vector2>();
        _isMove = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
    }

    void OnSlide(InputAction.CallbackContext context)
    {
        _isSlide = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJump = context.ReadValueAsButton();
    }

    #endregion

    private bool CheckGrounded()
    {
        Vector3 characterPosition = transform.position;

        Vector3 sphereCenter = characterPosition + Vector3.down * sphereOffset;
        bool isOnGround = Physics.SphereCast(sphereCenter, sphereRadius, Vector3.down, out RaycastHit hit, sphereOffset + 0.1f, _groundLayer);

        if (isOnGround)
        {
            Vector3 rayStart = characterPosition;
            Vector3 rayDirection = hit.point - rayStart;
            float rayDistance = Vector3.Distance(hit.point, rayStart) + 0.001f;

            if (Physics.Raycast(rayStart, rayDirection, out RaycastHit rayHit, rayDistance, _groundLayer))
            {
                if (CheckSloped())
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool CheckSloped()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized);
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }

    private void SpeedControl()
    {
        // limiting speed on slope
        if (IsSloped && !IsExitingSlope)
        {
            if (Rb.velocity.magnitude > MoveForce)
                Rb.velocity = Rb.velocity.normalized * MoveForce;
        }

        // limiting speed on ground or in air
        else
        {
            Vector3 flatVel = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

            // limit velocity if needed
            if (flatVel.magnitude > MoveForce)
            {
                Vector3 limitedVel = flatVel.normalized * MoveForce;
                Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
            }
        }
    }

    IEnumerator SmoovMoov()
    {
        float time = 0;
        float difference = Mathf.Abs(DesiredMoveForce - MoveForce);
        float startValue = MoveForce;

        while (time < difference)
        {
            MoveForce = Mathf.Lerp(startValue, DesiredMoveForce, time / difference);

            if (CheckSloped())
            {
                float slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * SpeedIncreaseMultiplier * SlopeSpeedIncreaseMultiplier * slopeAngleIncrease;
            }
            else
            {
                time += Time.deltaTime * SpeedIncreaseMultiplier;
            }

            yield return null;
        }

        MoveForce = DesiredMoveForce;
    }

    private void CheckForWall()
    {
        WallRight = Physics.Raycast(transform.position, Orientation.right, out rightWallhit, WallCheckDistance, _wallLayer);
        WallLeft = Physics.Raycast(transform.position, -Orientation.right, out leftWallhit, WallCheckDistance, _wallLayer);

    }

}