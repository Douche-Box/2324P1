using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnableMiniTanks : MonoBehaviour
{
    [SerializeField] NavMeshAgent[] agents;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>())
        {
            for (int i = 0; i < agents.Length; i++)
            {
                agents[i].enabled = true;
                agents[i].gameObject.GetComponent<Navmeshplayer>().Cando = true;
            }
        }
    }
}
