using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Lasers : MonoBehaviour
{
    [SerializeField] GameObject[] _lasers;

    [SerializeField] Navmeshplayer _agentcontroller;
    [SerializeField] NavMeshAgent _agent;

    [SerializeField] CharStateMachine _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            Debug.Log("hahdsfhasdf");
            _player = other.GetComponentInParent<CharStateMachine>();
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].SetActive(true);
            }
            _agentcontroller.Cando = true;
            _agent.enabled = true;
        }

    }

    private void Update()
    {
        if (_player != null && _player.HasDied)
        {
            for (int i = 0; i < _lasers.Length; i++)
            {
                _lasers[i].SetActive(false);
            }
            _agentcontroller.Cando = false;
            _agent.enabled = false;
        }
    }
}
