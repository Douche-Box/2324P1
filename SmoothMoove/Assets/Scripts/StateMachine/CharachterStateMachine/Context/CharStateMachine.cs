using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;
public class CharStateMachine : MonoBehaviour
{
    //CLEAN UP CODE
    #region Variables

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

    [SerializeField] Transform _playerCam;
    public Transform PlayerCam
    {
        get
        {
            return _playerCam;
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

    [SerializeField] Vector3 _jumpDirection;
    public Vector3 JumpDirection
    {
        get
        {
            return _jumpDirection;
        }
        set
        {
            _jumpDirection = value;
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

    [SerializeField] bool _isWallRunning;
    public bool IsWallRunning
    {
        get
        {
            return _isWallRunning;
        }
        set
        {
            _isWallRunning = value;
        }
    }

    [SerializeField] bool _isWalled;
    public bool IsWalled
    {
        get
        {
            return _isWalled;
        }
        set
        {
            _isWalled = value;
        }
    }

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
    [SerializeField] RaycastHit _rightWallHit;
    public RaycastHit RightWallHit
    {
        get
        {
            return _rightWallHit;
        }
    }

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
    [SerializeField] RaycastHit _leftWallHit;
    public RaycastHit LeftWallHit
    {
        get
        {
            return _leftWallHit;
        }
    }

    [SerializeField] LayerMask _wallLayer;

    [SerializeField] float _wallCheckDistance;
    public float WallCheckDistance
    {
        get
        {
            return _wallCheckDistance;
        }
    }

    [SerializeField] Vector3 _wallNormal;
    public Vector3 WallNormal
    {
        get
        {
            return _wallNormal;
        }
        set
        {
            _wallNormal = value;
        }
    }

    [SerializeField] Vector3 _wallForward;
    public Vector3 WallForward
    {
        get
        {
            return _wallForward;
        }
        set
        {
            _wallForward = value;
        }
    }

    [SerializeField] bool _canStartWallTimer;
    public bool CanStartWallTimer
    {
        get
        {
            return _canStartWallTimer;
        }
        set
        {
            _canStartWallTimer = value;
        }
    }

    [SerializeField] float _maxWallClingTime;
    public float MaxWallClingTime
    {
        get
        {
            return _maxWallClingTime;
        }
    }

    [SerializeField] float _wallClingTime;
    public float WallClingTime
    {
        get
        {
            return _wallClingTime;
        }
        set
        {
            _wallClingTime = value;
        }
    }

    [SerializeField] Transform _currentWall;
    public Transform CurrentWall
    {
        get
        {
            return _currentWall;
        }
        set
        {
            _currentWall = value;
        }
    }

    [SerializeField] Transform _previousWall;
    public Transform PreviousWall
    {
        get
        {
            return _previousWall;
        }
        set
        {
            _previousWall = value;
        }
    }

    #endregion

    [Header("Grappling")]
    #region Grappling

    [SerializeField] bool _isGrappled;
    public bool IsGrappled
    {
        get
        {
            return _isGrappled;
        }
        set
        {
            _isGrappled = value;
        }
    }

    [SerializeField] float _grappleDistance;
    public float GrappleDistance
    {
        get
        {
            return _grappleDistance;
        }
    }

    [SerializeField] LayerMask _grappleLayer;

    [SerializeField] RaycastHit _grappleHit;
    public RaycastHit GrappleHit
    {
        get
        {
            return _grappleHit;
        }
    }

    [SerializeField] SpringJoint _grappleJoint;
    public SpringJoint GrappleJoint
    {
        get
        {
            return _grappleJoint;
        }
        set
        {
            _grappleJoint = value;
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

    [SerializeField] bool _isAim;
    public bool IsAim
    {
        get
        {
            return _isAim;
        }
    }

    [SerializeField] bool _isShoot;
    public bool IsShoot
    {
        get
        {
            return _isShoot;
        }
    }

    #endregion

    [Header("Speeds")]
    #region Speeds

    [SerializeField] float _moveSpeed;
    public float MoveSpeed
    {
        get
        {
            return _moveSpeed;
        }
    }

    [SerializeField] float _airSpeed;
    public float AirSpeed
    {
        get
        {
            return _airSpeed;
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

    [SerializeField] float _grappleSpeed;
    public float GrappleSpeed
    {
        get
        {
            return _grappleSpeed;
        }
    }

    [Header("Speed Multipliers")]
    #region Speed Multipliers

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

    [SerializeField] float _grappleSpeedIncreaseMultiplier;
    public float GrappleSpeedIncreaseMultiplier
    {
        get
        {
            return _grappleSpeedIncreaseMultiplier;
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

    #endregion

    #endregion

    [SerializeField] float _startYScale;
    public float StartYScale
    {
        get
        {
            return _startYScale;
        }
    }

    [SerializeField] float _slideYScale;
    public float SlideYScale
    {
        get
        {
            return _slideYScale;
        }
    }

    [SerializeField] bool _isPushed;
    public bool IsPushed
    {
        get
        {
            return _isPushed;
        }
        set
        {
            _isPushed = value;
        }
    }

    [SerializeField] float _pushForce;
    public float PushForce
    {
        get
        {
            return _pushForce;
        }
        set
        {
            _pushForce = value;
        }
    }
    [SerializeField] float _maxPushTimer;

    [SerializeField] float _pushTimer;

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

        playerInput.actions.FindAction("Aim").started += OnAim;
        playerInput.actions.FindAction("Aim").performed += OnAim;
        playerInput.actions.FindAction("Aim").canceled += OnAim;

        playerInput.actions.FindAction("Shoot").started += OnShoot;
        playerInput.actions.FindAction("Shoot").performed += OnShoot;
        playerInput.actions.FindAction("Shoot").canceled += OnShoot;

        _states = new CharStateFactory(this);
        _currentState = _states.Grounded();
        _currentState.EnterState();
        _isGrounded = true;

        WallClingTime = MaxWallClingTime;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _startYScale = 1f;
    }

    #region MonoBehaviours

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            Debug.Log(_currentState.ToString());
        }


        if (_isShoot)
        {
            CheckForGrapple();
        }

        CurrentMovement = Orientation.forward * CurrentMovementInput.y + Orientation.right * CurrentMovementInput.x;

        _currentState.UpdateStates();

        IsGrounded = CheckGrounded();
        IsSloped = CheckSloped();
        CheckForWall();
        CheckWallDirection();

        if (CanStartWallTimer)
        {
            WallClingTime -= Time.deltaTime;
        }

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

    void OnAim(InputAction.CallbackContext context)
    {
        _isAim = context.ReadValueAsButton();
        Debug.Log("AIM");
    }

    void OnShoot(InputAction.CallbackContext context)
    {
        _isShoot = context.ReadValueAsButton();
        Debug.Log("SHOOT");
    }

    #endregion

    public void MakeGrappleJoint()
    {
        // if (GrappleJoint == null)
        //     return;

        Debug.Log(GrappleHit.point);

        GrappleJoint = this.transform.AddComponent<SpringJoint>();
        GrappleJoint.spring = 80000f;
        GrappleJoint.autoConfigureConnectedAnchor = false;
        GrappleJoint.connectedArticulationBody = GrappleHit.transform.GetComponent<ArticulationBody>();
        // GrappleJoint.connectedAnchor = GrappleHit.point;

        float distanceFromPoint = Vector3.Distance(this.transform.position, GrappleHit.point);

        GrappleJoint.maxDistance = distanceFromPoint * 0.4f;
        GrappleJoint.minDistance = distanceFromPoint * 0.25f;

        GrappleJoint.spring = 4.5f;
        GrappleJoint.damper = 7f;
        GrappleJoint.massScale = 4.5f;

    }

    public void DestroyGrapplePoint()
    {
        Destroy(GrappleJoint);
    }

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

    private void CheckForWall()
    {
        WallRight = Physics.Raycast(transform.position, Orientation.right, out _rightWallHit, WallCheckDistance, _wallLayer);
        WallLeft = Physics.Raycast(transform.position, -Orientation.right, out _leftWallHit, WallCheckDistance, _wallLayer);

        if (WallRight || WallLeft)
        {
            IsWalled = true;
        }
        else if (!WallRight && !WallLeft)
        {
            IsWalled = false;
        }
    }

    public void CheckWallDirection()
    {
        WallNormal = WallRight ? RightWallHit.normal : LeftWallHit.normal;

        WallForward = Vector3.Cross(WallNormal, transform.up);
    }

    public void CheckForGrapple()
    {
        if (Physics.Raycast(_playerCam.position, _playerCam.forward, out _grappleHit, GrappleDistance, _grappleLayer))
        {
            _isGrappled = true;
        }
        else
        {
            _isGrappled = false;
        }
    }

    public Vector3 GetWallJumpDirection(Vector3 inputDir)
    {
        Vector3 newInputDir = new Vector3(inputDir.x *= 4, 0, inputDir.z *= 4);
        newInputDir *= _jumpForce;
        return newInputDir;
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

            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY
            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY
            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY
            if (IsPushed && flatVel.magnitude > PushForce)
            {
                // IsPushed = false;
                Vector3 limitedVel = flatVel.normalized * PushForce;
                Rb.velocity = new Vector3(limitedVel.x, Rb.velocity.y, limitedVel.z);

                PushForce -= Time.deltaTime;

                if (PushForce <= MoveForce)
                {
                    IsPushed = false;
                    PushForce = 0;
                }
            }
            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY
            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY
            // DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY DO THIS DIFFERENTLY


            if (!IsPushed && flatVel.magnitude > MoveForce)
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

    private void OnDrawGizmos()
    {
        if (WallRight)
        {
            Debug.DrawRay(transform.position, Orientation.right, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, Orientation.right, Color.red);
        }

        if (WallLeft)
        {
            Debug.DrawRay(transform.position, -Orientation.right, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, -Orientation.right, Color.red);
        }
    }
}