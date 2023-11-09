using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 travelDirection;

    [SerializeField] float lifetime;

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    void Update()
    {
        transform.Translate(travelDirection * 50 * Time.deltaTime);
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(this.gameObject);
    }
}
