using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            _levelManager.EndGame();
        }
    }
}
