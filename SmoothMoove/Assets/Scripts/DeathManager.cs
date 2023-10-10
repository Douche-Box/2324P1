using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour
{
    [SerializeField] bool _hasDied;
    public bool HasDied
    {
        get
        {
            return _hasDied;
        }
        set
        {
            _hasDied = value;
        }
    }

    [SerializeField] Transform _resetPoint;
    public Transform ResetPoint
    {
        get
        {
            return _resetPoint;
        }
        set
        {
            _resetPoint = value;
        }
    }

    private void Update()
    {
        if (HasDied)
        {
            if (ResetPoint != null)
            {
                FindObjectOfType<CharStateMachine>().transform.position = ResetPoint.position;
            }
        }
    }
}
