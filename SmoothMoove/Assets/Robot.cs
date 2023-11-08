using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] int _robotHealth;
    [SerializeField] bool _roboHit;
    [SerializeField] Navmeshplayer _roboMover;
    [SerializeField] NavMeshAgent _roboNavmesh;
    [SerializeField] int _roboRebootTime;
    [SerializeField] Collider _hitbox;
    [SerializeField] Transform scaleer;

    [SerializeField] Animator _doorL;
    [SerializeField] Animator _doorR;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _roboHit = true;
        }
    }

    private void Update()
    {
        if (_roboHit)
        {
            _roboHit = false;
            _roboMover.Cando = false;
            _roboNavmesh.enabled = false;
            _hitbox.enabled = false;
            scaleer.localScale = new Vector3(scaleer.localScale.x, scaleer.localScale.y - 0.2f, scaleer.localScale.z);
            StartCoroutine(RobotReboot());
        }
        if (_robotHealth <= 0)
        {
            _doorL.SetBool("Open", true);
            _doorR.SetBool("Open", true);
            Destroy(scaleer.gameObject);
        }
    }

    IEnumerator RobotReboot()
    {
        yield return new WaitForSeconds(_roboRebootTime);
        _roboMover.Cando = true;
        _roboNavmesh.enabled = true;
        _hitbox.enabled = true;
        _robotHealth--;
    }
}
