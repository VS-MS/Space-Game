using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Unit {

    
    [SerializeField]
    private Vector2 m_velocite;
    //[SerializeField]
    //private LayerMask m_WhatIsGround;
    private Rigidbody2D m_Rigidbody2D;
    //private bool m_FacingRight = true;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_velocite = m_Rigidbody2D.velocity;//vivod skorost
        Debug.DrawLine(transform.position, transform.position + (Vector3)m_velocite);
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        m_Rigidbody2D.transform.Rotate(new Vector3(0, 0, - move * rotationSpeed));

    }
    public void Boost()
    {
        m_Rigidbody2D.AddForce(transform.up * boostForce);
        //m_Rigidbody2D.velocity = new Vector2(m_MaxSpeed, m_Rigidbody2D.velocity.y);
        //Debug.Log(boostForce);
    }

    public void Shoot()
    {
        Debug.Log("Tratatatatata");
    }

}
