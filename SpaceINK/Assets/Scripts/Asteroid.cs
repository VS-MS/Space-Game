using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int fragmentCount = 1; 
    public Asteroid fragmentAsteroid;

    private int countBullet = 0;

    public float torque;
    public float hp;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360));
        float randomNum = Random.Range(1, 3) == 1 ? Random.Range(7.0f, 10.0f) : Random.Range(-7.0f, -10.0f);
        rb.AddTorque(torque * randomNum);
    }

    public void ReceiveDamage(float damage) 
    {
        if(hp > damage)
        {
            hp -= damage;
        }
        if(hp <= damage)
        {
            countBullet ++;
            //Проверка на количество пуль, вызвавших ReciveDamage
            //исключаем повторный вызов создания астероидов
            if(countBullet <= 1 & fragmentCount != 0)
            {
                FindObjectOfType<AudioManager>().Play("Explosion5"); //Потом поменять на звук в эффекте взрыва
                for (int i = 0; i < fragmentCount; i++)
                {
                    Vector3 randomPosition = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0);
                    Asteroid newAsteroid = Instantiate(fragmentAsteroid, this.transform.position + randomPosition, Quaternion.Euler(0, 0, Random.Range(0, 360))) as Asteroid;
                    newAsteroid.GetComponent<Rigidbody2D>().AddForce(randomPosition * newAsteroid.torque * 100);
                }
                
            }
           
            Destroy(gameObject, 0.0f);
        }
    }

}
