using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHookPowerUpp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            other.GetComponentInParent<CharStateMachine>().GrappleHooks++;
        }
    }
}
