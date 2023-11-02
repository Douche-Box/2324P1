using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navmeshplayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //agent.speed = speed;
        //rend = GetComponent<Renderer>();

    }

    private void Update()
    {
        agent.destination = player.position;
        //agent.speed = speed * rend.material.SetFloat("_Speed");
        //rend.material.SetFloat("_Speed"

    }
}
