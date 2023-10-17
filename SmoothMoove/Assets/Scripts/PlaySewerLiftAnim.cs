using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySewerLiftAnim : MonoBehaviour
{
    [SerializeField] private Animator putDeksel = null;



    [SerializeField] private bool liftTrigger = false;


    public AudioSource audioSource;
    public AudioClip clip;
    public float volume;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (liftTrigger)
            {
                putDeksel.Play("Lift", 0, 0.0f);

                audioSource.PlayOneShot(clip, volume);
                
            }
            
        }
    }
    

}
