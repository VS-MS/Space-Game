using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemyCruiser : EnemyShip
{
    //вектор выходящий из данного объекта в корабль игрока
    private Vector3 heading;

    //Массив, где храним объекты ракетниц
    public RocketLauncherEnemy[] rocketArrayLeft;
    public RocketLauncherEnemy[] rocketArrayRight;

    private void Awake()
    {
        StatusSliderInt(6.0f, 2.0f);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        playerShip = GameObject.FindGameObjectWithTag("Player");
        if (startPoint == new Vector3(0, 0, 0))
        {
            startPoint = gameObject.transform.position;
        }
        moneyCount = SetMoneyCount();

        for (int i = 0; i < rocketArrayLeft.Length; i++)
        {
            rocketArrayLeft[i].bulletDamage = bulletDamage;
            rocketArrayLeft[i].bulletSpeed = bulletSpeed;
        }

        for (int i = 0; i < rocketArrayRight.Length; i++)
        {
            rocketArrayRight[i].bulletDamage = bulletDamage;
            rocketArrayRight[i].bulletSpeed = bulletSpeed;
        }
    }

    private void FixedUpdate()
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

    private void Chase()
    {

        //Вычисляем угол между данным объектом и кораблем игрока в градах
        float angle = CalculateAngle(gameObject.transform.position);
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

    private void Shoot()
    {
        rocketArrayLeft[Random.Range(0, rocketArrayLeft.Length)].ShootTurret();
        rocketArrayRight[Random.Range(0, rocketArrayRight.Length)].ShootTurret();
    }
}
