using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float torque;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        rb.AddTorque(torque * Random.Range(-10.0f, 10.0f));
    }

    void FixedUpdate()
    {
        //float turn = Input.GetAxis("Horizontal");
    }
}
