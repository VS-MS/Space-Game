using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float torque;
    public float hp;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        rb.AddTorque(torque * Random.Range(-10.0f, 10.0f));
    }

    public void ReceiveDamage(float damage) 
    {
        if(hp > damage)
        {
            hp -= damage;
        }
        if(hp <= damage)
        {
            Destroy(gameObject, 0.0f);
        }
    }

}
