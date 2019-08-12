using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    public GameObject laserParticle;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyBullet")
        {
            GameObject splash = Instantiate(laserParticle, collision.transform.position, transform.rotation);
            splash.transform.parent = this.transform;
            Destroy(splash, 2.0f);
        }
    }

}
