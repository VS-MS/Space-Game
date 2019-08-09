using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{

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
        RefreshStat();
    }
    //Скрипт для апгрейда cannon... и т.д для каждой кнопки
    public void CannonUp()
    {
        if(DataSave.instance.CannonLvl < 20)
        {
            if (CoastUpgrade(DataSave.instance.CannonLvl) <= DataSave.instance.money)
            {
                DataSave.instance.money -= CoastUpgrade(DataSave.instance.CannonLvl);
                DataSave.instance.CannonLvl++;
                DataSave.instance.SaveGame();
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
        if (CoastUpgrade(DataSave.instance.SuperShotLvl) <= DataSave.instance.money)
        {
            DataSave.instance.money -= CoastUpgrade(DataSave.instance.SuperShotLvl);
            DataSave.instance.SuperShotLvl++;
            DataSave.instance.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void ArmorShieldUp()
    {
        if (CoastUpgrade(DataSave.instance.ArmorShieldLvl) <= DataSave.instance.money)
        {
            DataSave.instance.money -= CoastUpgrade(DataSave.instance.ArmorShieldLvl);
            DataSave.instance.ArmorShieldLvl++;
            DataSave.instance.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void EngineUp()
    {
        if (CoastUpgrade(DataSave.instance.EngineLvl) <= DataSave.instance.money)
        {
            DataSave.instance.money -= CoastUpgrade(DataSave.instance.EngineLvl);
            DataSave.instance.EngineLvl++;
            DataSave.instance.SaveGame();
            RefreshStat();
        }
        else
        {
            Debug.Log("Dude, you are need more money for this");
        }
    }

    public void SuperBoostUp()
    {
        if (CoastUpgrade(DataSave.instance.SuperBoostLvl) <= DataSave.instance.money)
        {
            DataSave.instance.money -= CoastUpgrade(DataSave.instance.SuperBoostLvl);
            DataSave.instance.SuperBoostLvl++;
            DataSave.instance.SaveGame();
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
        if (DataSave.instance.CannonLvl == 20)
            cannonLvlUpCost.text = "Max";
        else
            cannonLvlUpCost.text = numberToString.ShortNumber(CoastUpgrade(DataSave.instance.CannonLvl));  //CoastUpgrade(dataSave.CannonLvl).ToString();

        //Super Shoot
        if (DataSave.instance.SuperShotLvl == 20)
            superShootLvlUpCost.text = "Max";
        else
            superShootLvlUpCost.text = CoastUpgrade(DataSave.instance.SuperShotLvl).ToString();

        //Armor Shield
        if (DataSave.instance.ArmorShieldLvl == 20)
            armorShieldLvlUpCost.text = "Max";
        else
            armorShieldLvlUpCost.text = CoastUpgrade(DataSave.instance.ArmorShieldLvl).ToString();

        //Engine
        if (DataSave.instance.EngineLvl == 20)
            engineLvlUpCost.text = "Max";
        else
            engineLvlUpCost.text = CoastUpgrade(DataSave.instance.EngineLvl).ToString();

        //SuperBoost
        if (DataSave.instance.SuperBoostLvl == 20)
            superBoostLvlUpCost.text = "Max";
        else
            superBoostLvlUpCost.text = CoastUpgrade(DataSave.instance.SuperBoostLvl).ToString();

        //статы коробля
        //Главня пушка
        cannonDamageText.text = DataSave.instance.cannonDamage.ToString("0.00");
        cannonRateText.text = DataSave.instance.cannonFireRate.ToString("0.00");
        cannonSpeedText.text = DataSave.instance.cannonBulletSpeed.ToString("0.00");
        cannonCountText.text = DataSave.instance.cannonCount.ToString();
        //SuperShoot
        ssDamageText.text = DataSave.instance.ssDamage.ToString("0%");
        ssMaxTimeText.text = DataSave.instance.ssMaxTime.ToString("0");
        ssReloadText.text = DataSave.instance.ssTimeReload.ToString("0.00");
        //Armor Shield
        armorText.text = DataSave.instance.shipArmor.ToString("0");
        shieldText.text = DataSave.instance.shipShield.ToString("0");
        shieldDeltaText.text = DataSave.instance.shipShieldDelta.ToString("0.00");
        //Engine
        engineMaxSpeedText.text = DataSave.instance.shipMaxSpeed.ToString("0.00");
        engineAccelerationText.text = DataSave.instance.shipAcceleration.ToString("0.00");
        engineRotationText.text = DataSave.instance.shipRotation.ToString("0.00");
        //Super Boost
        sbMaxSpeedText.text = DataSave.instance.sbMaxSpeed.ToString("0%");
        sbAccelerationText.text = DataSave.instance.sbAcceleration.ToString("0%");
        sbMaxTime.text = DataSave.instance.sbMaxTime.ToString("0");
        sbReloadText.text = DataSave.instance.sbTimeReload.ToString("0.00");


    }
}
