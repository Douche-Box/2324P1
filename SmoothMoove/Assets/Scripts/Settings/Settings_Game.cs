using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings_Game : MonoBehaviour
{
    public GameObject char_Cam;

    public float sens;
    public float fov;

    public bool change_Sens;
    public bool change_FoV;

private void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            change_FoV = !change_FoV;
        }
        if (change_FoV) {

            fov = char_Cam.GetComponent<Camera>().fieldOfView;

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                fov++;

                char_Cam.GetComponent<Camera>().fieldOfView = fov;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                fov--;

                char_Cam.GetComponent<Camera>().fieldOfView = fov;
            }
        }
        if (Input.GetButtonDown("Fire1")) {
            change_Sens = !change_Sens;
        }
        if (change_Sens) {
            sens = char_Cam.GetComponent<Char_Cam>().sens;
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                sens++;

                char_Cam.GetComponent<Char_Cam>().sens = sens;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                sens--;

                char_Cam.GetComponent<Char_Cam>().sens = sens;
            }
        }
    }
}