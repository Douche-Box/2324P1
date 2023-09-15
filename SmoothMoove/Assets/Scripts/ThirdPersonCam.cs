using System.Collections;
using System.Collections.Generic;
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

        Vector3 inputDir = _orientation.forward * _stateMachine.CurrentMovement.z + _orientation.right * _stateMachine.CurrentMovement.y;

        if (inputDir != Vector3.zero)
        {
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, Time.deltaTime * _rotationSpeed);
        }


    }
}
