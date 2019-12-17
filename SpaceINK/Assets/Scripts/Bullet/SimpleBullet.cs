using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet: Bullet
{
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.fixedDeltaTime);
    }
    private void Update() 
    {
        
        //Debug.Log(speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Unit>().armorPoints > 0)
            {
                collision.gameObject.GetComponent<Unit>().ReceiveDamage(damage);

                GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
                splash.transform.parent = collision.transform;
                Destroy(splash, 2.0f);

                //Destroy(gameObject);
                gameObject.SetActive(false);
            }
            
        }
        if (collision.tag == "Asteroid")
        {
            
            GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
            Destroy(splash, 2.0f);

            //Destroy(gameObject);
            gameObject.SetActive(false);

        }
    }

    

}
