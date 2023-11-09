using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulletprojectile : MonoBehaviour
{
   
    public GameObject spawnPosObj;
    public GameObject Bullet;
    public GameObject PlaneObject;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = (GameObject)Instantiate(Bullet, spawnPosObj.transform.position, Quaternion.identity);

            bullet.GetComponent<BulletScript>().travelDirection = PlaneObject.transform.forward;
        }
    }
}
