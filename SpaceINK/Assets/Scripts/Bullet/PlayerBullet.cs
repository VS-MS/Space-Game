using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet
{
    public TrailRenderer trail;
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //Двигаем пулю по космосу без физики
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(transform.position, transform.position + direction, step);
    }

    private void OnEnable()
    {
        if(trail)
        {
            trail.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyShip")
        {
            //Проверяем, есть ли у цели ХП, чтобы у него его отнять
            if (collision.gameObject.GetComponent<Unit>().armorPoints > 0)
            {
                //Передаем урон объекту
                collision.gameObject.GetComponent<Unit>().ReceiveDamage(damage);

                //ПЕРЕДЕЛАТЬ!!!!! ДОбавить эффект взрыва снаряда в пул объектов
                GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
                splash.transform.parent = collision.transform;
                Destroy(splash, 2.0f);

                //Делаем снаряд не активным, тем самым возвращаем его обратно в пул объектов
                gameObject.SetActive(false);
            }
            
        }
        if (collision.tag == "Asteroid")
        {
            collision.gameObject.GetComponent<Asteroid>().ReceiveDamage(damage);

            GameObject splash = Instantiate(laserParticle, transform.position, transform.rotation);
            Destroy(splash, 2.0f);

            //Делаем снаряд не активным, тем самым возвращаем его обратно в пул объектов
            gameObject.SetActive(false);
        }
    }
}
