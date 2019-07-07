using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunEnemy : MonoBehaviour
{

    public float bulletSpeed = 0.1f;
    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    private float myTime = 0.5F;//время прошло от последнего выстрела
    public SimpleBullet simpleBullet;

    private Transform gunTransform;

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


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootTurret()
    {
        if (myTime > fireDelta)
        {

            SimpleBullet newBullet = Instantiate(simpleBullet, gunTransform.position, simpleBullet.transform.rotation) as SimpleBullet;
            newBullet.Speed = bulletSpeed;
            newBullet.Parent = gameObject;
            newBullet.Direction = this.transform.up;
            newBullet.Damage = 1.0f;
            myTime = 0.0F;
        }
    }
}
