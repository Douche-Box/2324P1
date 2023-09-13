using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Move : MonoBehaviour
{
    public Transform cam_Pos;

    void Update()
    {
        transform.position = cam_Pos.position;
    }
}
