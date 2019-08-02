using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private DataSave dataSave;

    [Header("Button Text")]
    public TextMeshProUGUI cannonLvlUpCost;
    public TextMeshProUGUI superShootLvlUpCost;
    public TextMeshProUGUI armorShieldLvlUpCost;
    public TextMeshProUGUI engineLvlUpCost;
    public TextMeshProUGUI superBoostLvlUpCost;

    [Header("Cannon Stat Text")]
    public TextMeshProUGUI cannonDamageText;
    public TextMeshProUGUI cannonRateText;
    public TextMeshProUGUI cannonSpeedText;
    public TextMeshProUGUI cannonCountText;

    [Header("Super Shoot Text")]
    public TextMeshProUGUI ssDamageText;
    public TextMeshProUGUI ssMaxTimeText; 
    public TextMeshProUGUI ssReloadText;

    [Header("Armor Shield Text")]
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI shieldText;
    public TextMeshProUGUI shieldDeltaText;

    [Header("Engnie Text")]
    public TextMeshProUGUI engineMaxSpeedText;
    public TextMeshProUGUI engineAccelerationText;
    public TextMeshProUGUI engineRotationText;

    [Header("SUper Boost Text")]
    public TextMeshProUGUI sbMaxSpeedText;
    public TextMeshProUGUI sbAccelerationText;
    public TextMeshProUGUI sbMaxTime;
    public TextMeshProUGUI sbReloadText;

    private Desc_ numberToString = new Desc_();
    public void Awake()
    {
        
    }
    public void Start()
    {
        dataSave = FindObjectOfType<DataSave>();
        RefreshStat();
    }

    public void Update()
    {
        dataSave = FindObjectOfType<DataSave>();
        //Debug.Log(dataSave.GetInstanceID() + "Upgrade.cs");
    }
    //Скрипт для апгрейда cannon... и т.д для каждой кнопки
    public void CannonUp()
    {
        if(dataSave.CannonLvl < 20)
        {
            if (CoastUpgrade(dataSave.CannonLvl) <= dataSave.money)
            {
                dataSave.money -= CoastUpgrade(dataSave.CannonLvl);
                dataSave.CannonLvl++;
                dataSave.SaveGame();
                RefreshStat();
            }
            else
            {
                Debug.Log("Dude, you are need more money for this");
            }
        }
        else
        {
            Debug.Log("Max level reached");
        }
        
    }

    public void SuperShootUp()
    {
        if (CoastUpgrade(dataSave.SuperShotLvl) <= dataSave.money)
        {
            dataSave.money -= CoastUpgrade(dataSave.SuperShotLvl);
            dataSave.SuperShotLvl++;
            dataSave.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void ArmorShieldUp()
    {
        if (CoastUpgrade(dataSave.ArmorShieldLvl) <= dataSave.money)
        {
            dataSave.money -= CoastUpgrade(dataSave.ArmorShieldLvl);
            dataSave.ArmorShieldLvl++;
            dataSave.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void EngineUp()
    {
        if (CoastUpgrade(dataSave.EngineLvl) <= dataSave.money)
        {
            dataSave.money -= CoastUpgrade(dataSave.EngineLvl);
            dataSave.EngineLvl++;
            dataSave.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void SuperBoostUp()
    {
        if (CoastUpgrade(dataSave.SuperBoostLvl) <= dataSave.money)
        {
            dataSave.money -= CoastUpgrade(dataSave.SuperBoostLvl);
            dataSave.SuperBoostLvl++;
            dataSave.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    //метод для высчитывания стоимости
    public int CoastUpgrade (int level)
    {
        int coast = 50;
        for (int i = 0; i < level; i++)
        {
            coast = 2 * coast;
        }
        return coast;
    }

    public void RefreshStat()
    {
        //Цена апгрейда на кнопках
        //Cannon
        if (dataSave.CannonLvl == 20)
            cannonLvlUpCost.text = "Max";
        else
            cannonLvlUpCost.text = numberToString.ShortNumber(CoastUpgrade(dataSave.CannonLvl));  //CoastUpgrade(dataSave.CannonLvl).ToString();

        //Super Shoot
        if (dataSave.SuperShotLvl == 20)
            superShootLvlUpCost.text = "Max";
        else
            superShootLvlUpCost.text = CoastUpgrade(dataSave.SuperShotLvl).ToString();

        //Armor Shield
        if (dataSave.ArmorShieldLvl == 20)
            armorShieldLvlUpCost.text = "Max";
        else
            armorShieldLvlUpCost.text = CoastUpgrade(dataSave.ArmorShieldLvl).ToString();

        //Engine
        if (dataSave.EngineLvl == 20)
            engineLvlUpCost.text = "Max";
        else
            engineLvlUpCost.text = CoastUpgrade(dataSave.EngineLvl).ToString();

        //SuperBoost
        if (dataSave.SuperBoostLvl == 20)
            superBoostLvlUpCost.text = "Max";
        else
            superBoostLvlUpCost.text = CoastUpgrade(dataSave.SuperBoostLvl).ToString();

        //статы коробля
        //Главня пушка
        cannonDamageText.text = dataSave.cannonDamage.ToString("0.00");
        cannonRateText.text = dataSave.cannonFireRate.ToString("0.00");
        cannonSpeedText.text = dataSave.cannonBulletSpeed.ToString("0.00");
        cannonCountText.text = dataSave.cannonCount.ToString();
        //SuperShoot
        ssDamageText.text = dataSave.ssDamage.ToString("0%");
        ssMaxTimeText.text = dataSave.ssMaxTime.ToString("0");
        ssReloadText.text = dataSave.ssTimeReload.ToString("0.00");
        //Armor Shield
        armorText.text = dataSave.shipArmor.ToString("0");
        shieldText.text = dataSave.shipShield.ToString("0");
        shieldDeltaText.text = dataSave.shipShieldDelta.ToString("0.00");
        //Engine
        engineMaxSpeedText.text = dataSave.shipMaxSpeed.ToString("0.00");
        engineAccelerationText.text = dataSave.shipAcceleration.ToString("0.00");
        engineRotationText.text = dataSave.shipRotation.ToString("0.00");
        //Super Boost
        sbMaxSpeedText.text = dataSave.sbMaxSpeed.ToString("0%");
        sbAccelerationText.text = dataSave.sbAcceleration.ToString("0%");
        sbMaxTime.text = dataSave.sbMaxTime.ToString("0");
        sbReloadText.text = dataSave.sbTimeReload.ToString("0.00");


    }
}
