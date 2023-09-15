using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class CharStateMachine : MonoBehaviour
{
    [SerializeField] TMP_Text _speedText;
    //CLEAN UP CODE
    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;

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

    [SerializeField] LayerMask groundLayer;
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
    }

    #region MonoBehaviours

    private void Update()
    {
        _speedText.text = "speed:" + Rb.velocity.magnitude;
        _currentState.UpdateStates();

        IsGrounded = CheckGrounded();
        IsSloped = CheckSloped();

        // DO THIS IN GROUNDED STATE NOT IN STATE MACHINE
        if (IsGrounded)
        {
            Rb.drag = GroundDrag;
        }
        else
        {
            Rb.drag = 0;
        }

        // MAKE THIS MORE GENERAL FOR MORE CONTROLL OF EACH SITUATION || DO THIS IN EVERY STATE INSTEAD OF DOING IT HERE
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
        _isMove = _currentMovementInput.x != 0 || _currentMovementInput.y != 0;
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

    // NEEDS CODE CHECKUP && cleanup
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

    // NEEDS CODE CHECKUP || NEEDS ITS OWN STATE
    bool CheckSloped()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, _playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < _maxSlopeAngle && angle != 0;
        }

        return false;
    }

    // NEEDS TO BE MORE GENERAL FOR MORE CONTROL || USE THIS IN EVERY STATE
    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(Rb.velocity.x, 0f, Rb.velocity.z);

        if (flatVel.magnitude > MoveForce)
        {
            Vector3 limitedVel = flatVel.normalized * MoveForce;
            Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);
        }
    }
}
