using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreadnoughtShip : EnemyShip
{
    //вектор выходящий из данного объекта в корабль игрока
    protected Vector3 heading;

    //Массив, где храним объекты турелей
    public GameObject[] turretArray;
    [SerializeField]
    protected float turretRotationSpeed;
    //Массив, где храним объекты ракетниц
    [SerializeField]
    protected RocketLauncherEnemy[] rocketArrayLeft;
    [SerializeField]
    protected RocketLauncherEnemy[] rocketArrayRight;

    protected void Awake()
    {
        InitStat();

        StatusSliderInt(6.0f, 2.0f);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        if (startPoint == new Vector3(0, 0, 0))
        {
            startPoint = gameObject.transform.position;
        }
        moneyCount = SetMoneyCount();

        for (int i = 0; i < turretArray.Length; i++)
        {
            turretArray[i].GetComponent<TurretEnemy>().fireDelta = fireDelta;
            turretArray[i].GetComponent<TurretEnemy>().bulletSpeed = bulletSpeed;
        }

        for (int i = 0; i < rocketArrayLeft.Length; i++)
        {
            rocketArrayLeft[i].fireDelta = fireDelta * 8;
            rocketArrayLeft[i].armorPoints = armorPoints / 40;
            rocketArrayLeft[i].bulletDamage = bulletDamage * 2;
            rocketArrayLeft[i].bulletSpeed = bulletSpeed;
        }

        for (int i = 0; i < rocketArrayRight.Length; i++)
        {
            rocketArrayRight[i].fireDelta = fireDelta * 8;
            rocketArrayRight[i].armorPoints = armorPoints / 40;
            rocketArrayRight[i].bulletDamage = bulletDamage * 2;
            rocketArrayRight[i].bulletSpeed = bulletSpeed;
        }

    }

    protected private void FixedUpdate()
    {
        if (shipState == State.Die)
        {
            //отключаем все коллайдеры на объекте
            foreach (Collider2D collider in this.GetComponents<Collider2D>())
            {
                collider.enabled = false;
            }
            //отключаем спрайты
            this.transform.Find("SpriteRender").gameObject.SetActive(false);


        }
        else
        {
            if (myTime <= fireDelta) //считаемвремя до след выстрела
            {
                myTime = myTime + Time.deltaTime;
            }

            if (shieldTime <= shieldDelta) //считаем до восстановления щита
            {
                shieldTime += Time.deltaTime;
            }
            else//если таймер пройдет, проверяем, нужно ли восстановить щит
            {
                if (shieldPoints < maxShieldPoints)
                {
                    shieldPoints += 1; //восстанавливаем щит на еденицу
                }
                shieldTime = 0;//обнуляем счетчик
            }

            if (playerShip && playerShip.activeSelf)
            {
                //heading - вектор выходящий из данного объекта в корабль игрока
                heading = playerShip.transform.position - gameObject.transform.position;

                if (heading.sqrMagnitude < radarRadius * radarRadius) //проверяем, попал корабль игрока в зону радара
                {
                    state = State.Attack;
                    Chase();
                    Shoot();
                }
                else
                {
                    state = State.Idle;
                    FollowingPoint(startPoint);
                }
                //DebugLine();
            }
            else
            {
                state = State.Idle;
                FollowingPoint(startPoint);
            }
        }

    }

    protected void Shoot()
    {
        //стреляем из турели
        int i = Random.Range(0, turretArray.Length);
        
        turretArray[i].GetComponent<TurretEnemy>().ShootTurret();
        //стреляем из ракетниц
        rocketArrayLeft[Random.Range(0, rocketArrayLeft.Length)].ShootTurret();
        rocketArrayRight[Random.Range(0, rocketArrayRight.Length)].ShootTurret();
    }



    protected void Chase()
    {

        //Вычисляем угол между данным объектом и кораблем игрока в градах
        float angle = CalculateAngle(gameObject.transform.position);

        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

        //Поворачиваем башни в сторону цели
        for (int i = 0; i < turretArray.Length; i++)
        {
            //Вычисляем угол между данным объектом(турель) и кораблем игрока в градах
            angle = CalculateAngle(turretArray[i].transform.position);
            q = Quaternion.AngleAxis(angle, Vector3.forward);
            turretArray[i].transform.rotation = Quaternion.Slerp(turretArray[i].transform.rotation, q, Time.deltaTime * turretRotationSpeed);
        }

        //Ускоряемсяв сторону коробля игрока
        //СИла ускорения зависит от растояния до игрока, чем ближе, тем она меньше(пока не работает, слишком вялые противники с такой опцией)
        if (/*distance >=2 &&*/ m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        {
            //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * (boostForce * (heading.magnitude / radarRadius)));
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
    }
}
