using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathReset : MonoBehaviour
{
    [SerializeField] DeathManager _deathManager;

    private void Awake()
    {
        _deathManager = FindObjectOfType<DeathManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _deathManager.HasDied = true;
        }
    }
}
