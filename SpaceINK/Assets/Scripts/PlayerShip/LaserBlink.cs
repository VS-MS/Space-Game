using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlink : MonoBehaviour
{
    public GameObject laserParticle;
    public string laserTag; 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == laserTag)
        {
            GameObject splash = Instantiate(laserParticle, collision.transform.position, transform.rotation);
            splash.transform.parent = this.transform;
            Destroy(splash, 2.0f);
        }
        
    }

}
