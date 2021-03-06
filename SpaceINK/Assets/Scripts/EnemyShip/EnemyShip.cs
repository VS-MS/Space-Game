﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyShip : Unit
{
    //peremennaya v kotoroi hranim max znachenie yrovnei
    private float maxLvl = 24;

    //Радиус обнаружения противника
    public float radarRadius = 30;
    //Радиус начала стрельбы
    public float attackRadius;
    //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
    protected Vector3 targetingPosition;
    protected GameObject playerShip;
    protected Vector3 targetSpeed;
    public float bulletDamage = 3; 
    [SerializeField]
    protected float bulletSpeed = 1;
    [SerializeField]
    private EnemyStatusSlider enemyStatusSlider;
    public EnemyStatusSlider statusSlider;

    //начальная позиция коробля, используется для возврата, если цель не найдена
    public Vector3 startPoint;

    //начальное и текущее состояние
    protected State state = State.Idle;

    protected Rigidbody2D m_Rigidbody2D;

    public float fireDelta = 0.70F;//скорость стрельбы
    //private float nextFire = 0.5F;
    protected float myTime = 0.5F;//время прошло от последнего выстрела

    [SerializeField]
    private BaseEnemyShipStat baseEnemyStat;
    [SerializeField]
    private BaseEnemyShipStat maxEnemyStat;

    protected int SetMoneyCount()
    {
        int money_;
        //Высчитываем количество денег за уничтожение коробля
        //Чем больше ХП и Урон, тем больше денег
        money_ = (int)((maxArmorPoints + maxShieldPoints) * bulletDamage);

        string s = SceneManager.GetActiveScene().name;
        int lvlNumber;
        try
        {
            lvlNumber = Convert.ToInt32(s);
            //DataSave.instance.levelComplite = lvlNumber;
        }
        catch (System.FormatException)
        {
            Debug.LogError("Уровень не может быть засчитан. Не верное название сцены, сцена должна называться только целочисленным числом.");
            lvlNumber = 1;
        }

        //Возвращаем деньги, чем выше лвл уровня, тем больше множитель
        return money_ + money_* (lvlNumber/4);
    }
    //Устанавливаем значение для слайдера над кораблем ХП и Щит
    public void StatusSliderInt(float _upDis, float _scaleX) 
    {
        //На старте у корабля максимальное значение ХП и Щита,
        //Сохраняем их в переменную, чтобы потом высчитывать соотношение
        //для полоски ХП и Щита
        maxArmorPoints = armorPoints;
        maxShieldPoints = shieldPoints;
        //Создаем слайдер над кораблем и устанавливаем начальные значения
        EnemyStatusSlider newStatusSlider = Instantiate(enemyStatusSlider, this.transform.position, this.transform.rotation) as EnemyStatusSlider;
        newStatusSlider.maxArmor = armorPoints;
        newStatusSlider.maxShield = shieldPoints;
        newStatusSlider.enemyShip = this;
        newStatusSlider.upDis = _upDis;
        newStatusSlider.SetScaleX(_scaleX);
        statusSlider = newStatusSlider;
    }


    protected virtual float CalculateAngle(Vector3 targetFrom)
    {
        var headingAim = CalculateAim() - targetFrom;
        //distance - растояние от данного объекта до корабль игрока
        var distance = headingAim.magnitude;
        //direction - направление от данного объекта до игрока
        var direction = headingAim / distance;

        //Вычисляем угол между данным объектом и кораблем игрока в градах
        return Mathf.Atan2(headingAim.y, headingAim.x) * Mathf.Rad2Deg - 90;
    }

    protected virtual Vector3 CalculateAim()
    {
        //По умолчанию турель стреляет прямо по цели, но, если цель движется, то нужно высчитать точку,
        //которая находится перед движущейся целью и по которой будет стрелять турель.
        //То есть турель должна стрелять на опережение
        targetingPosition = playerShip.transform.position;
        //Узнаем скорость цели
        targetSpeed = playerShip.GetComponent<Rigidbody2D>().velocity;
        //Debug.Log("PlayerShipVelosity = " + targetSpeed);
        //Высчитываем точку, перед мишенью, по которой нужно произвести выстрел, чтобы попасть по движущейся мишени
        //по идее, чем больше итераций, тем точнее будет положение точки для упреждающего выстрела
        for (int i = 0; i < 4; i++)
        {
            float dist = (transform.position - targetingPosition).magnitude;
            //Debug.Log("dist = " + dist);
            float timeToTarget = dist / (bulletSpeed);
            targetingPosition = playerShip.transform.position + targetSpeed * timeToTarget;
        }

        return targetingPosition;
    }

    protected void FollowingPoint(Vector3 point)
    {
        //вектор от коробля до точки следования
        var headingAim = point - gameObject.transform.position;
        //находим растояние
        var distance = headingAim.magnitude;
        //вектор направления
        var direction = headingAim / distance;
        //Вычисляем угол между данным объектом и точкой следования
        float angle = Mathf.Atan2(headingAim.y, headingAim.x) * Mathf.Rad2Deg - 90;
        //Ищем квантарион этого угла
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        //плавно прварачиваем объект
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
        //if (m_Rigidbody2D.velocity.magnitude <= maxSpeed)
        if((m_Rigidbody2D.transform.up * maxSpeed + (Vector3)m_Rigidbody2D.velocity).magnitude <= maxSpeed * 2)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * boostForce);
        }
    }

    public void DebugLine()
    {
        if (state == State.Attack)
        {
            Debug.DrawLine(transform.position, playerShip.transform.position, Color.red);
            Debug.DrawLine(transform.position, CalculateAim(), Color.yellow);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radarRadius);
    }


    //Устанавливаем статы корабля в зависимости от загруженного уровня
    //Чем выше уровень сцены, тем выше статы корабля
    public float CalculatStat(float firstStat, float lastStat)
    {
        string s = SceneManager.GetActiveScene().name;
        int lvlNumber;
        try
        {
            lvlNumber = Convert.ToInt32(s);
        }
        catch (System.FormatException)
        {
            lvlNumber = 1;
            Debug.LogError("Не верное название сцены, сцена должна называться только целочисленным числом. Установленно значение по умолчанию равное = " + lvlNumber);
        }

        float step;
        float lvlStat;
        step = Mathf.Abs(Mathf.Sqrt(firstStat) - Mathf.Sqrt(lastStat)) / (maxLvl - 1.0f);
        if (firstStat < lastStat)
        {
            lvlStat = Mathf.Pow(Mathf.Sqrt(firstStat) + (step * (lvlNumber - 1)), 2);
        }
        else
        {
            lvlStat = Mathf.Pow(Mathf.Sqrt(firstStat) - (step * (lvlNumber - 1)), 2);
        }
        return lvlStat;
    }


    protected void InitStat()
    {
       
        armorPoints = CalculatStat(baseEnemyStat.armorPoints, maxEnemyStat.armorPoints);
        maxArmorPoints = CalculatStat(baseEnemyStat.maxArmorPoints, maxEnemyStat.maxArmorPoints);

        shieldPoints = CalculatStat(baseEnemyStat.shieldPoints, maxEnemyStat.shieldPoints);
        maxShieldPoints = CalculatStat(baseEnemyStat.maxShieldPoints, maxEnemyStat.maxShieldPoints);

        shieldDelta = CalculatStat(baseEnemyStat.shieldDelta, maxEnemyStat.shieldDelta);

        maxSpeed = CalculatStat(baseEnemyStat.maxSpeed, maxEnemyStat.maxSpeed);
        boostForce = CalculatStat(baseEnemyStat.boostForce, maxEnemyStat.boostForce);
        rotationSpeed = CalculatStat(baseEnemyStat.rotationSpeed, maxEnemyStat.rotationSpeed);

        bulletDamage = CalculatStat(baseEnemyStat.bulletDamage, maxEnemyStat.bulletDamage);
        bulletSpeed = CalculatStat(baseEnemyStat.bulletSpeed, maxEnemyStat.bulletSpeed);
        fireDelta = CalculatStat(baseEnemyStat.fireDelta, maxEnemyStat.fireDelta);

    }
}
