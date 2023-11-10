using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutAnimReset : MonoBehaviour
{
    [SerializeField] CharStateMachine _player;

    [SerializeField] Animator _put;
    private void Awake()
    {
        _player = FindObjectOfType<CharStateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            _put.SetBool("Open", false);
        }
    }
}
