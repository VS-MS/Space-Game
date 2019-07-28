using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private DataSave dataSave;

    [Header("Button Text")]
    public TextMeshProUGUI cannonLvl;
    public TextMeshProUGUI superShootLvl;
    public TextMeshProUGUI armorShieldLvl;
    public TextMeshProUGUI engineLvl;
    public TextMeshProUGUI superBoostLvl;

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

    public void Awake()
    {
        
    }
    public void Start()
    {
        dataSave = FindObjectOfType<DataSave>();
        RefreshStat();
    }

    public void CannonUp()
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

    public int CoastUpgrade (int level)
    {
        int coast = 500;
        for (int i = 0; i < level; i++)
        {
            coast = 2 * coast;
        }
        return coast;
    }

    public void RefreshStat()
    {
        //Цена апгрейда на кнопка
        cannonLvl.text = CoastUpgrade(dataSave.CannonLvl).ToString();
        superShootLvl.text = CoastUpgrade(dataSave.SuperShotLvl).ToString();
        armorShieldLvl.text = CoastUpgrade(dataSave.ArmorShieldLvl).ToString();
        engineLvl.text = CoastUpgrade(dataSave.EngineLvl).ToString();
        superBoostLvl.text = CoastUpgrade(dataSave.SuperBoostLvl).ToString();

        //статы коробля
        //Главня пушка
        cannonDamageText.text = dataSave.cannonDamage.ToString();
        cannonRateText.text = dataSave.cannonFireRate.ToString();
        cannonSpeedText.text = dataSave.cannonBulletSpeed.ToString();
        cannonCountText.text = dataSave.cannonCount.ToString();
        //SuperShoot
        ssDamageText.text = dataSave.ssDamage.ToString();
        ssMaxTimeText.text = dataSave.ssMaxTime.ToString();
        ssReloadText.text = dataSave.ssTimeReload.ToString();
        //Armor Shield
        armorText.text = dataSave.shipArmor.ToString();
        shieldText.text = dataSave.shipShield.ToString();
        shieldDeltaText.text = dataSave.shipShieldDelta.ToString();
        //Engine
        engineMaxSpeedText.text = dataSave.shipMaxSpeed.ToString();
        engineAccelerationText.text = dataSave.shipAcceleration.ToString();
        engineRotationText.text = dataSave.shipRotation.ToString();
        //Super Boost
        sbMaxSpeedText.text = dataSave.sbMaxSpeed.ToString();
        sbAccelerationText.text = dataSave.sbAcceleration.ToString();
        sbMaxTime.text = dataSave.sbMaxTime.ToString();
        sbReloadText.text = dataSave.sbTimeReload.ToString();


    }
}
