using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] DeathManager _deathManager;

    [SerializeField] Transform _checkPoint;

    private void Awake()
    {
        _deathManager = FindObjectOfType<DeathManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _deathManager.ResetPoint = _checkPoint;
            _deathManager.HasDied = false;
        }
    }
}
