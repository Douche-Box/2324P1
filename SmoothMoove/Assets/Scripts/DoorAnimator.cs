using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{

    // [SerializeField] CharStateMachine _player;
    // [SerializeField] float _distanceRequired;
    // [SerializeField] float distance;

    [SerializeField] Animator _animationDoorLeft;
    [SerializeField] Animator _animationDoorRight;

    [SerializeField] bool _openClose;

    // private void Awake()
    // {
    //     _player = FindObjectOfType<CharStateMachine>();
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            _openClose = !_animationDoorLeft.GetBool("Open");

            _animationDoorLeft.SetBool("Open", _openClose);
            _animationDoorRight.SetBool("Open", _openClose);
        }
    }

    // private void Update()
    // {
    //     // distance = Vector3.Distance(_player.transform.position, this.transform.position);
    //     // _openClose = (Vector3.Distance(_player.transform.position, this.transform.position) < _distanceRequired);
    // }
}
