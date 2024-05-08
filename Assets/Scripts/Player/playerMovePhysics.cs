using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovePhysics : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float force = 100f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.forward * force);
    }
}
