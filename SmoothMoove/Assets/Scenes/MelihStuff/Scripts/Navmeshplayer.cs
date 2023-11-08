using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navmeshplayer : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    [SerializeField] bool _cando;
    public bool Cando
    {
        get { return _cando; }
        set { _cando = value; }
    }
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        //agent.speed = speed;
        //rend = GetComponent<Renderer>();

    }

    private void Update()
    {
        if (_cando)
        {
            agent.destination = player.position;
        }

        //agent.speed = speed * rend.material.SetFloat("_Speed");
        //rend.material.SetFloat("_Speed"

    }
}
