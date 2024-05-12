using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera cam;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(cam.transform);
    }
}
