using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_FoV : MonoBehaviour
{
    public Camera cam;

    public float fov;

    public bool change_FoV;

    private void Update()
    {
        if (Input.GetButtonDown("Jump")) {
            change_FoV = !change_FoV;
        }

        if (change_FoV) {

            fov = cam.fieldOfView;

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
                fov++;

                cam.fieldOfView = fov;
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) {
                fov--;

                cam.fieldOfView = fov;
            }
        }
    }
}
