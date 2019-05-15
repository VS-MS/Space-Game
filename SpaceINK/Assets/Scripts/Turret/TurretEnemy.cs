using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    private float myTime = 0.5F;//время прошло от последнего выстрела
    public SimpleBullet simpleBullet;  

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (myTime <= fireDelta) //считаемвремя до след выстрела
        {
            myTime = myTime + Time.deltaTime;
        }
    }

    public void ShootTurret() 
    {
        if (myTime > fireDelta)
        {
            //nextFire = myTime + fireDelta;
            Vector3 position = this.transform.position;
            //position.y += 0.4F;
            SimpleBullet newBullet = Instantiate(simpleBullet, position, simpleBullet.transform.rotation) as SimpleBullet;
            newBullet.Speed = bulletSpeed;
            newBullet.Parent = gameObject;
            //newBullet.Direction = newBullet.transform.right * (-direction.x < 0.0F ? 1.0F : -1.0F) + new Vector3(0f, Random.Range(-0.10f, 0.20f), 0f);  //Направление полета пули(магия логических выражений) выражение которое проверяем ? что сделать1 если true: что сделать 2 если false
            newBullet.Direction = this.transform.up;
            //Debug.Log(m_Rigidbody2D.velocity.magnitude);

            //newBullet.color = buletcolor;
            newBullet.Damage = 0.1f;

            //nextFire = nextFire - myTime;
            myTime = 0.0F;
        }
    }
}
