using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharStateMachine : MonoBehaviour
{
    //CLEAN UP CODE
    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;

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

    int _zero = 0;

    [SerializeField] Rigidbody _rb;
    public Rigidbody Rb
    {
        get
        {
            return _rb;
        }
    }

    [SerializeField] Transform _rotation;
    public Transform Rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation = value;
        }
    }

    [SerializeField] float _moveForce;
    public float MoveForce
    {
        get
        {
            return _moveForce;
        }
    }

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

    [SerializeField] float _speed;
    public float Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            _speed = value;
        }
    }

    [SerializeField] float _capSpeed;
    public float CapSpeed
    {
        get
        {
            return _capSpeed;
        }
        set
        {
            _capSpeed = value;
        }
    }

    [SerializeField] float _jumpForce;
    public float JumpForce
    {
        get
        {
            return _jumpForce;
        }
    }

    [Header("Groundcheck")]
    #region GroundCheck
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float sphereRadius;
    [SerializeField] float sphereOffset;
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

    [SerializeField] bool _isRun;
    public bool IsRun
    {
        get
        {
            return _isRun;
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

    [SerializeField] float _groundDrag;
    public float GroundDrag
    {
        get
        {
            return _groundDrag;
        }
    }

    private void Awake()
    {
        playerInput.actions.FindAction("Move").started += OnMovement;
        playerInput.actions.FindAction("Move").performed += OnMovement;
        playerInput.actions.FindAction("Move").canceled += OnMovement;

        playerInput.actions.FindAction("Run").started += OnRun;
        playerInput.actions.FindAction("Run").performed += OnRun;
        playerInput.actions.FindAction("Run").canceled += OnRun;

        playerInput.actions.FindAction("Jump").started += OnJump;
        playerInput.actions.FindAction("Jump").performed += OnJump;
        playerInput.actions.FindAction("Jump").canceled += OnJump;

        _states = new CharStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
        IsGrounded = true;
    }

    #region MonoBehaviours

    private void Update()
    {
        _currentState.UpdateStates();

        IsGrounded = CheckGrounded();

        if (IsGrounded)
        {
            Rb.drag = GroundDrag;
        }
        else
        {
            Rb.drag = 0;
        }

        SpeedControl();
    }

    private void FixedUpdate()
    {
        _currentState.FixedUpdateStates();
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
        _isMove = _currentMovementInput.x != _zero || _currentMovementInput.y != _zero;
    }

    void OnRun(InputAction.CallbackContext context)
    {
        _isRun = context.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        _isJump = context.ReadValueAsButton();
    }

    #endregion

    // CLEAN UP
    private bool CheckGrounded()
    {
        Vector3 characterPosition = transform.position;

        Vector3 sphereCenter = characterPosition + Vector3.down * sphereOffset;
        bool isOnGround = Physics.SphereCast(sphereCenter, sphereRadius, Vector3.down, out RaycastHit hit, sphereOffset + 0.1f, groundLayer);

        if (isOnGround)
        {
            Vector3 rayStart = characterPosition;
            Vector3 rayDirection = hit.point - rayStart;
            float rayDistance = Vector3.Distance(hit.point, rayStart) + 0.001f;

            if (Physics.Raycast(rayStart, rayDirection, out RaycastHit rayHit, rayDistance, groundLayer))
            {
                Debug.DrawRay(rayStart, rayDirection, Color.green);
                return true;
            }
            else
            {
                Debug.DrawRay(rayStart, rayDirection, Color.red);
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

        if (flatVel.magnitude > MoveForce)
        {
            Vector3 limitedVel = flatVel.normalized * MoveForce;
            Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
        }
    }
    // bool OnSlope()
    // {
    //     if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
    //     {
    //         float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
    //         return angle < maxSlopeAngle && angle != 0;
    //     }

    //     return false;
    // }
}
