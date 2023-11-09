using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    [SerializeField] Animator[] _animations;

    [SerializeField] bool _openClose;

    [SerializeField] bool _bossDoor;
    [SerializeField] GameObject _boss;
    [SerializeField] bool _hasAnimated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharStateMachine>() && !_bossDoor && !_hasAnimated)
        {
            _hasAnimated = true;
            for (int i = 0; i < _animations.Length; i++)
            {
                _openClose = !_animations[i].GetBool("Open");
                _animations[i].SetBool("Open", _openClose);

            }
        }
    }

    private void Update()
    {
        if (_bossDoor && _boss == null)
        {
            for (int i = 0; i < _animations.Length; i++)
            {
                _animations[i].SetBool("Open", true);
            }
        }
    }
}
