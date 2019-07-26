using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    private DataSave dataSave;

    public TextMeshProUGUI cannonLvl;
    public TextMeshProUGUI superShootLvl;
    public TextMeshProUGUI armorShieldLvl;
    public TextMeshProUGUI engineLvl;
    public TextMeshProUGUI superBoostLvl;

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
        cannonLvl.text = CoastUpgrade(dataSave.CannonLvl).ToString();
        superShootLvl.text = CoastUpgrade(dataSave.SuperShotLvl).ToString();
        armorShieldLvl.text = CoastUpgrade(dataSave.ArmorShieldLvl).ToString();
        engineLvl.text = CoastUpgrade(dataSave.EngineLvl).ToString();
        superBoostLvl.text = CoastUpgrade(dataSave.SuperBoostLvl).ToString();
    }
}
