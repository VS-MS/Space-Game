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
        float randomNum = Random.Range(1, 3) == 1 ? Random.Range(7.0f, 10.0f) : Random.Range(-7.0f, -10.0f);
        rb.AddTorque(torque * randomNum);
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
