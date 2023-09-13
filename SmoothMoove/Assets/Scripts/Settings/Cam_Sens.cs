using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Sens : MonoBehaviour
{
    public GameObject char_Cam;

    public float sens;

    public bool change_Sens;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            change_Sens = !change_Sens;
        }

        if (change_Sens)
        {
            sens = char_Cam.GetComponent<Char_Cam>().sens;
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                sens++;

                char_Cam.GetComponent<Char_Cam>().sens = sens;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                sens--;

                char_Cam.GetComponent<Char_Cam>().sens = sens;
            }
        }
    }
}