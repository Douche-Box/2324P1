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

    // Update is called once per frame
    void Update()
    {
        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        Vector3 inputDir = _orientation.forward * _stateMachine.CurrentMovementInput.y + _orientation.right * _stateMachine.CurrentMovementInput.x;

        inputDir.y = 0;

        if (_stateMachine.IsWallRunning)
        {
            if ((_stateMachine.PlayerObj.forward - _stateMachine.WallForward).magnitude > (_stateMachine.PlayerObj.forward - -_stateMachine.WallForward).magnitude)
            {
                _stateMachine.WallForward = new Vector3(-_stateMachine.WallForward.x, -_stateMachine.WallForward.y, -_stateMachine.WallForward.z);
            }
            _orientation.forward = _stateMachine.WallForward;
            Quaternion wallLookRotation = Quaternion.LookRotation(_stateMachine.WallForward.normalized, Vector3.up);
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, wallLookRotation, Time.deltaTime);
        }
        if (inputDir != Vector3.zero && !_stateMachine.IsWallRunning)
        {
            Quaternion lookRotation = Quaternion.LookRotation(inputDir, Vector3.up);
            _playerObj.transform.rotation = Quaternion.Slerp(_playerObj.transform.rotation, lookRotation, Time.deltaTime * _rotationSpeed);
        }


    }
}
