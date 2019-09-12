using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGunEnemy : MonoBehaviour
{
    public float bulletSpeed = 0.1f;
    public float bulletDamage = 3;
    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    protected float myTime = 0.5F;//время прошло от последнего выстрела

    public Transform[] gunTransform;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
