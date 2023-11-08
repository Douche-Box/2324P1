using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathReset : MonoBehaviour
{
    [SerializeField] DeathManager _deathManager;

    [SerializeField] AudioClip _laserDeathSound;
    [SerializeField] AudioClip _buzzsawDeathSound;


    public enum DeathType
    {
        FALL,
        DROWN,
        LASER,
        EXPLOSION,
        BUZZSAW,
    }

    [SerializeField] DeathType deathType;
    private void Awake()
    {
        _deathManager = FindObjectOfType<DeathManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(" DEATH PLANE ");
            // switch (deathType)
            // {
            //     case DeathType.FALL:
            // AudioSource.PlayClipAtPoint(_laserDeathSound, _deathManager.Player.transform.position);
            //         break;
            //     case DeathType.LASER:
            //         break;
            //     case DeathType.EXPLOSION:
            //         break;
            //     case DeathType.BUZZSAW:
            // AudioSource.PlayClipAtPoint(_buzzsawDeathSound, _deathManager.Player.transform.position);
            //         break;
            // }
            _deathManager.DoDeath();
        }
    }
}
