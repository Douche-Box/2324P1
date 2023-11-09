using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepSoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource _source;

    [SerializeField] AudioClip[] _clipsMetal;
    [SerializeField] AudioClip[] _clipsWood;
    [SerializeField] AudioClip[] _clipsForceField;
    [SerializeField] AudioClip[] _clipsConcrete;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
            Debug.Log("Metal");
            _source.PlayOneShot(_clipsMetal[Random.Range(0, _clipsMetal.Length)]);
        }
        else if (other.CompareTag("Wood"))
        {
            Debug.Log("Wood");

            _source.PlayOneShot(_clipsWood[Random.Range(0, _clipsWood.Length)]);
        }
        else if (other.CompareTag("ForceField"))
        {
            Debug.Log("ForceField");

            _source.PlayOneShot(_clipsForceField[Random.Range(0, _clipsForceField.Length)]);

        }
        else if (other.CompareTag("Concrete"))
        {
            Debug.Log("Concrete");

            _source.PlayOneShot(_clipsConcrete[Random.Range(0, _clipsConcrete.Length)]);
        }
    }
}
