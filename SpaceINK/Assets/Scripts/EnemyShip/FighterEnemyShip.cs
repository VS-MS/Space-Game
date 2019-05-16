using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterEnemyShip : EnemyShip
{

    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;

    [SerializeField]
    private SimpleBullet simpleBullet;
    //начальная позиция коробля, используется для возврата, если цель не найдена
    private Vector3 startPoint;

    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    private float myTime = 0.5F;//время прошло от последнего выстрела

    private Transform gunTransform;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        startPoint = transform.position;
        gunTransform = this.transform.Find("Gun_1");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Shoot()
    {    
        if (myTime > fireDelta)
        {
            SimpleBullet newBullet = Instantiate(simpleBullet, gunTransform.position, simpleBullet.transform.rotation) as SimpleBullet;
            newBullet.Speed = bulletSpeed;
            newBullet.Parent = gameObject;
            newBullet.Direction = m_Rigidbody2D.transform.up;

            //newBullet.color = buletcolor;
            newBullet.Damage = 0.1f;
            myTime = 0.0F;
        }
    }
    private void FixedUpdate()
    {
        if (myTime <= fireDelta) //считаемвремя до след выстрела
        {
            myTime = myTime + Time.deltaTime;
        }

        if (playerShip)
        {
            //heading - вектор выходящий из данного объекта в корабль игрока
            heading = playerShip.transform.position - gameObject.transform.position;

            if (heading.sqrMagnitude < radarRadius * radarRadius) //проверяем, попал корабль игрока в зону радара
            {
                state = State.Atack;
                Chase();
                Shoot();
            }
            else
            {
                state = State.Idle;
                FollowingPoint(startPoint);
            }
            DebugLine();
        }
        else
        {
            state = State.Idle;
            FollowingPoint(startPoint);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        
        
    }

    //Погоня за кораблем противника, и удержание его на расстоянии выстрела
    //Корабль поварачиваем в сторону предпологаемого выстрела на опережение
    private void Chase()
    {
        
        var headingAim = CalculateAim() - gameObject.transform.position;
        //distance - растояние от данного объекта до корабль игрока
        var distance = headingAim.magnitude;
        //direction - направление от данного объекта до игрока
        var direction = headingAim / distance;

        //Вычисляем угол между данным объектом и кораблем игрока в градах
        float angle = Mathf.Atan2(headingAim.y, headingAim.x) * Mathf.Rad2Deg - 90;

        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        //Ускоряемсяв сторону коробля игрока
        //СИла ускорения зависит от растояния до игрока, чем ближе, тем она меньше(пока не работает, слишком вялые противники с такой опцией)
        if (/*distance >=2 &&*/ m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (boostForce * (heading.magnitude / radarRadius)));
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
        
    }

}
