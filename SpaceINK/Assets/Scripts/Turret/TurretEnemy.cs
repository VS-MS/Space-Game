using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : UnitGunEnemy
{
    
    public SimpleBullet simpleBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        gunTransform = this.transform.Find("Gun_1");
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
            newBullet.Damage = bulletDamage;
            myTime = 0.0F;
        }
    }
}
