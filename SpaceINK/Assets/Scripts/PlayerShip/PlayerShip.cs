using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Unit {

    [SerializeField]
    private PlayerBullet playerBullet;
    [SerializeField]
    private float bulletSpeed = 80f;
    public float fireDelta = 0.70F;//скорость стрельбы
    private float myTime = 0.5F;//время прошло от последнего выстрела

    [SerializeField]
    private Vector2 m_velocite;
    //[SerializeField]
    private Rigidbody2D m_Rigidbody2D;
    private float rotationTime;
    public float RotationTime { set { if (value >= 0 & value <= 1) rotationTime = value; else rotationTime = 0.01f; } }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_velocite = m_Rigidbody2D.velocity;//vivod skorost
        Debug.DrawLine(transform.position, transform.position + (Vector3)m_velocite);
    }

    private void FixedUpdate()
    {
        if (myTime <= fireDelta) //считаемвремя до след выстрела
        {
            myTime = myTime + Time.deltaTime;
        }
    }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float move)
    {
        //Переменная для поворота с ускорением
        if(rotationTime < 1f)
        {
            rotationTime += Time.deltaTime;
            Debug.Log(rotationTime);
        }
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.transform.rotation * Quaternion.Euler(0f, 0f, rotationSpeed * move), rotationTime);
    }



    public void Boost()
    {
        if(m_Rigidbody2D.velocity.magnitude < maxSpeed)
        {
            m_Rigidbody2D.AddForce(transform.up * boostForce);
        }
    }

    public void Shoot()
    {
        if (myTime > fireDelta)
        {
            //nextFire = myTime + fireDelta;

            Vector3 position = m_Rigidbody2D.transform.position;
            //position.y += 0.4F;
            PlayerBullet newBullet = Instantiate(playerBullet, position, playerBullet.transform.rotation) as PlayerBullet;
            newBullet.Speed = bulletSpeed;
            newBullet.Parent = gameObject;
            //newBullet.Direction = newBullet.transform.right * (-direction.x < 0.0F ? 1.0F : -1.0F) + new Vector3(0f, Random.Range(-0.10f, 0.20f), 0f);  //Направление полета пули(магия логических выражений) выражение которое проверяем ? что сделать1 если true: что сделать 2 если false
            newBullet.Direction = m_Rigidbody2D.transform.up;
            //Debug.Log(m_Rigidbody2D.velocity.magnitude);

            //newBullet.color = buletcolor;
            newBullet.Damage = 10;

            //nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }

}
