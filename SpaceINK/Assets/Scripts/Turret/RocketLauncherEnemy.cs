using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherEnemy : UnitGunEnemy 
{

    public EnemyRocket simpleRocket;
    
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

            EnemyRocket newRocket = Instantiate(simpleRocket, gunTransform.position, this.transform.rotation) as EnemyRocket;
            newRocket.rocketSpeed = bulletSpeed;
            Debug.Log(this.transform.rotation);
            //newRocket.Parent = gameObject;
            newRocket.rocketDamage = 3.0f;
            myTime = 0.0F;
        }
    }
}
