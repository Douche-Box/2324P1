using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DistanceAnimation : MonoBehaviour
{
    [SerializeField] Animator[] _animation;

    [SerializeField] float _distanceRequired;

    [SerializeField] CharStateMachine _player;

    [SerializeField] float distance;

    private void Awake()
    {
        _player = FindObjectOfType<CharStateMachine>();
    }

    private void Update()
    {
        distance = Vector3.Distance(_player.transform.position, this.transform.position);
        if (Vector3.Distance(_player.transform.position, this.transform.position) < _distanceRequired)
        {
            for (int i = 0; i < _animation.Length; i++)
            {
                _animation[i].SetBool("Open", true);
            }
        }
        else if (Vector3.Distance(_player.transform.position, this.transform.position) > _distanceRequired)
        {
            for (int i = 0; i < _animation.Length; i++)
            {
                _animation[i].SetBool("Open", false);
            }
        }
    }
}
