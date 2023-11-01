using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    [SerializeField] Transform _fanTransform;

    [SerializeField] float _pushForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<CharStateMachine>().IsForced = true;
            other.GetComponentInParent<CharStateMachine>().ExtraForce = _pushForce;

            other.GetComponentInParent<CharStateMachine>().Rb.velocity = new Vector3(other.GetComponentInParent<CharStateMachine>().Rb.velocity.x, 0, other.GetComponentInParent<CharStateMachine>().Rb.velocity.z);
            other.GetComponentInParent<CharStateMachine>().Rb.AddForce(_fanTransform.up *= _pushForce, ForceMode.Impulse);
        }
    }
}
