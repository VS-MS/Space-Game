using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject laserParticle;
    private GameObject parent;
    public GameObject Parent { set { parent = value; } }

    private float speed = 150.0f;
    public float Speed { set { if (value > 0) speed = value; else speed = 0; } }
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    /*
    private Transform rotationBullet;
    public Transform RotationBullet { set { rotationBullet = value; } }
    */

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

    private TrailRenderer trail;
    public float trailWidth = 0.25f;
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        trail = GetComponentInChildren<TrailRenderer>();

    }

    private void OnEnable()
    {
        //transform.rotation = parent.transform.rotation;
        StartCoroutine("TimeToDie");
    }

    private void OnDisable()
    {
        StopCoroutine("TimeToDie");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator TimeToDie()
    {  
        yield return new WaitForSeconds(4.1f);
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //float step = speed * Time.deltaTime;
        //transform.position = Vector3.Lerp(transform.position, transform.position + direction, step);

        //trail.startWidth = trailWidth;
    }

    void FixedUpdate()
    {
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, step);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShip")
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
            collision.gameObject.GetComponent<Asteroid>().ReceiveDamage(damage);

            GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
            Destroy(splash, 2.0f);

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
