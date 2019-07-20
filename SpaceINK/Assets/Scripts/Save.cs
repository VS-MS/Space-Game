
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
/*
 * Класс Save используется для сериализации и сохранения на устройстве
 */
{
    public int money;
    public int levelComplite;

    public float canonDamage;
    public float canonFireRate;
    public float canonSpeed;
    public int canonCount;

    public float ssDamage;
    public float ssFireRate;
    public float ssMaxTime;
    public float ssTimeReload;

    public float shipArmor;
    //public float shipArmorDelta;
    public float shipShield;
    public float shipShieldDelta;

    public float shipMaxSpeed;
    public float shipAcceleration;
    public float shipRotation;

    public float sbMaxSpeed;
    public float sbAcceleration;
    public float sbMaxTime;
    public float sbTimeReload;
}
 