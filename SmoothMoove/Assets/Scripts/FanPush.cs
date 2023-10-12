using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanPush : MonoBehaviour
{
    [SerializeField] Transform _fanTransform;

    [SerializeField] float _pushForce;

    [SerializeField] List<Rigidbody> _rbs = new List<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _rbs.Add(other.GetComponentInParent<Rigidbody>());

            other.GetComponentInParent<CharStateMachine>().IsForced = true;
            other.GetComponentInParent<CharStateMachine>().ExtraForce = _pushForce;
        }
        if (other.GetComponent<Rigidbody>())
        {
            _rbs.Add(other.GetComponent<Rigidbody>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            if (_rbs.Contains(other.GetComponent<Rigidbody>()))
            {
                _rbs.Remove(other.GetComponent<Rigidbody>());
            }
        }
        if (other.CompareTag("Player"))
        {
            if (_rbs.Contains(other.GetComponentInParent<Rigidbody>()))
            {
                _rbs.Remove(other.GetComponentInParent<Rigidbody>());
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody rb in _rbs)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(_fanTransform.up *= _pushForce, ForceMode.Impulse);
        }
    }
}
