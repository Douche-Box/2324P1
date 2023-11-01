using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCam : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput = null;
    public PlayerInput PlayerInput => playerInput;

    [SerializeField]
    private Transform _orientation,
    _player,
    _playerObj;

    [SerializeField]
    private Rigidbody _rb;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private CharStateMachine _stateMachine;


    public enum CamState
    {
        ground,
        air,
        jump,
        wallrun,
    }
    public CamState camState;

    [SerializeField] float inputY;
    [SerializeField] float inputX;

    [SerializeField] float oldInputY;
    [SerializeField] float oldInputX;


    Vector3 inputDir;

    Quaternion currentCameraRotation;
    Quaternion previousCameraRotation;

    [SerializeField] float _rotationThreshold;

    void Update()
    {
        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        inputDir = _orientation.forward * _stateMachine.CurrentMovementInput.y + _orientation.right * _stateMachine.CurrentMovementInput.x;
        inputY = _stateMachine.CurrentMovementInput.y;
        inputX = _stateMachine.CurrentMovementInput.x;

        inputDir.y = 0;

        currentCameraRotation = transform.rotation;
        float rotationChange = Quaternion.Angle(currentCameraRotation, previousCameraRotation);

        if (_stateMachine.IsAired && rotationChange > _rotationThreshold)
        {
            Quaternion lookRotation = Quaternion.LookRotation(viewDir, Vector3.up);
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            previousCameraRotation = currentCameraRotation;
        }
        else if (_stateMachine.IsWallRunning)
        {
            if ((_stateMachine.PlayerObj.forward - _stateMachine.WallForward).magnitude > (_stateMachine.PlayerObj.forward + _stateMachine.WallForward).magnitude)
            {
                _stateMachine.WallForward = new Vector3(-_stateMachine.WallForward.x, -_stateMachine.WallForward.y, -_stateMachine.WallForward.z);
            }
            _orientation.forward = _stateMachine.WallForward;
            Quaternion wallLookRotation = Quaternion.LookRotation(_stateMachine.WallForward.normalized, Vector3.up);
            _playerObj.transform.rotation = wallLookRotation;
        }
        else if (_stateMachine.IsSliding)
        {
            Quaternion slopeAdjustedRotation = Quaternion.FromToRotation(Vector3.up, _stateMachine._slopeHit.normal);
            Quaternion lookRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            Quaternion finalRotation = slopeAdjustedRotation * lookRotation;
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, finalRotation, Time.deltaTime * _rotationSpeed);

        }
        else if (inputDir != Vector3.zero)
        {


            if (inputY == -oldInputY && inputY == 1f || inputY == -oldInputY && inputY == -1f || inputX == -oldInputX && inputX == 1f || inputX == -oldInputX && inputX == -1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(inputDir, Vector3.up);
                _playerObj.transform.rotation = lookRotation;
            }
            else
            {
                Quaternion lookRotation = Quaternion.LookRotation(inputDir, Vector3.up);
                _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
            }

        }

        if (inputY != 0)
        {
            oldInputY = inputY;
            oldInputX = inputX;
        }
        if (inputX != 0)
        {
            oldInputX = inputX;
            oldInputY = inputY;
        }
    }
}
