using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherEnemy : UnitGunEnemy 
{

    public EnemyRocket simpleRocket;
    public float armorPoints = 1;

    public override void ShootTurret()
    {


        if (myTime > fireDelta)
        {
            
            for (int i = 0; i < gunTransform.Length; i++)
            {
                EnemyRocket newRocket = Instantiate(simpleRocket, gunTransform[i].position, this.transform.rotation) as EnemyRocket;
                newRocket.maxSpeed = bulletSpeed;
                newRocket.armorPoints = armorPoints;
                newRocket.rocketDamage = bulletDamage;
            }
            
            myTime = 0.0F;
        }
    }
}
