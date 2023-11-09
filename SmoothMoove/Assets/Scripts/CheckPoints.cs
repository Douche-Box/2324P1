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
        _checkPoint = this.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" CHECK POINT ");
            _deathManager.DoCheckPoint(_checkPoint);
        }
    }
}
