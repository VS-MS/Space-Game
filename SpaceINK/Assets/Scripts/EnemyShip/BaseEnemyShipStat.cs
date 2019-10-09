using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat/BaseEnemyStat", fileName = "BaseEnemyStat")]

public class BaseEnemyShipStat : ScriptableObject
{
    [Header("Health Settings")]
    //Уровень хп брони корабля
    public float armorPoints = 100;
    public float maxArmorPoints = 100;
    //Уровень щита корабля
    public float shieldPoints = 100;
    public float maxShieldPoints = 100;
    //счетчик для востановления щита
    public float shieldDelta = 8f;

    [Header("Speed Settings")]
    public float maxSpeed = 10f;                     // максимальная скорость, которую может развить корабль
    public float boostForce = 400f;                  // ускорение корабля
    public float rotationSpeed = 1f;				 //Скорость вращения корабля 

    [Header("Atack settings")]
    public float bulletDamage = 3;
    public float bulletSpeed = 1;
    public float fireDelta = 0.70F;//скорость стрельбы
}
