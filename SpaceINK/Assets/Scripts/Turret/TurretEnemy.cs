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

    private Transform gunTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gunTransform = this.transform.Find("Gun_1");
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

            SimpleBullet newBullet = Instantiate(simpleBullet, gunTransform.position, simpleBullet.transform.rotation) as SimpleBullet;
            newBullet.Speed = bulletSpeed;
            newBullet.Parent = gameObject;
            newBullet.Direction = this.transform.up;
            //newBullet.color = buletcolor;
            newBullet.Damage = 0.1f;
            myTime = 0.0F;
        }
    }
}
