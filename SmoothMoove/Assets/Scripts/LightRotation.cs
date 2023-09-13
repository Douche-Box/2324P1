using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float hallo;
    public float g;
    public float s;
    // Start is called before the first frame update
    void Start()
    {
       hallo = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(g, hallo, s * Time.deltaTime); ;
    }
}
