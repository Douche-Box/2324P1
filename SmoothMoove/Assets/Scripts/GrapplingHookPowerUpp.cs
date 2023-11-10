using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookPowerUpp : MonoBehaviour
{
    [SerializeField] CharStateMachine _player;
    [SerializeField] GameObject _model;


    private void Awake()
    {
        _player = FindObjectOfType<CharStateMachine>();
    }

    private void Update()
    {
        if (_player.HasDied)
        {
            this.gameObject.GetComponent<Collider>().enabled = true;
            _model.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            other.GetComponentInParent<CharStateMachine>().GrappleHooks++;
            this.gameObject.GetComponent<Collider>().enabled = false;
            _model.SetActive(false);
        }
    }
}
