using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stat/BasePlayerStat", fileName = "BasePlayerStat")]
public class BasePlayerStat : ScriptableObject
{
    [Header("Cannon")]
    public float cannonDamage;
    public float cannonFireRate;
    public float cannonBulletSpeed;
    public int cannonCount;

    [Header("Super Shot")]
    public float ssDamage;
    public float ssMaxTime;
    public float ssTimeReload;

    [Header("Armor/Shield")]
    public float shipArmor;
    //public float shipArmorDelta;
    public float shipShield;
    public float shipShieldDelta;

    [Header("Engine")]
    public float shipMaxSpeed;
    public float shipAcceleration;
    public float shipRotation;

    [Header("Super Boost")]
    public float sbMaxSpeed;
    public float sbAcceleration;
    public float sbMaxTime;
    public float sbTimeReload;
}
