using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet: MonoBehaviour
{
    public GameObject laserParticle;
    private GameObject parent;
    public GameObject Parent { set { parent = value; } }

    private float speed = 150.0f;
    public float Speed { set { if (value > 0) speed = value; else speed = 1; } } 
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    [SerializeField]
    private float damage = 1.0f;
    public float Damage { set { damage = value; } }
    private SpriteRenderer sprite;

    private int maxKillCount = 1;
    public int MaxKillCount { set { maxKillCount = value; } }

    private int killCount = 0;

    public Color color
    {
        set { sprite.color = value; }
    }

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, 5.5f);
        transform.rotation = parent.transform.rotation;
    }

    private void Update() 
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
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

                Destroy(gameObject);
            }
            
        }
        if (collision.tag == "Asteroid")
        {
            
            GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
            Destroy(splash, 2.0f);

            Destroy(gameObject);
            
        }
    }

    

}
