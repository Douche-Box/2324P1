using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 travelDirection;

    void Update()
    {
        transform.Translate(travelDirection * 50 * Time.deltaTime);
    }
}
