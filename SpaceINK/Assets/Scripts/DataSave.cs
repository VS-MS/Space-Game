using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataSave : MonoBehaviour 
{
    [SerializeField]
    private BasePlayerStat basePlayerStat;
    [SerializeField]
    private BasePlayerStat maxPlayerStat;

    public int money; 
    public int levelComplite;

    public int CannonLvl; 
    public int SuperShotLvl;
    public int ArmorShieldLvl;
    public int EngineLvl;
    public int SuperBoostLvl;

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

    //public Save PlayerStat;

    static bool created = false;
    void Awake()
    {
        
        //Проверяем, есть ли экземпляр объекта на сцене, если есть, удаляем этот экземпляр
        if (!created)
        {
            //DontDestroyOnLoad не удоляет объект при загрузки другой сцены.
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Debug.Log("Destroy");
            Destroy(this.gameObject);
        }
        LoadGame();//Так делать не стоит, но пока нет законченного меню, придется оставить.
    }

    public void Update()
    {
        Debug.Log(this.GetInstanceID() + " this is real dataSave");
    }
    //функция для расчета изменения параметров от минимальных(firstStat) 
    //до максимальных(lastStat) значений в зависимости от уровня(дlvl).
    //Шаг считается по квадратичной функции X^2
    public float CalculatStat(float firstStat, float lastStat, int lvl) 
    {
        float step;
        float lvlStat;
        step = Mathf.Abs(Mathf.Sqrt(firstStat) - Mathf.Sqrt(lastStat)) / 19.0f;
        if(firstStat < lastStat)
        {
            lvlStat = Mathf.Pow(Mathf.Sqrt(firstStat) + (step * (lvl - 1)), 2);
        }
        else
        {
            lvlStat = Mathf.Pow(Mathf.Sqrt(firstStat) - (step * (lvl - 1)),2);
        }
        return lvlStat;
    }

    public void RefreshStat()
    {
        //Cannon
        cannonDamage = CalculatStat(basePlayerStat.cannonDamage, maxPlayerStat.cannonDamage, CannonLvl);
        cannonFireRate = CannonLvl * basePlayerStat.cannonFireRate;
        cannonBulletSpeed = CannonLvl * basePlayerStat.cannonBulletSpeed;
        cannonCount = CannonLvl * basePlayerStat.cannonCount;

        //Super Shot
        ssDamage = SuperShotLvl * basePlayerStat.ssDamage;
        ssMaxTime = SuperShotLvl * basePlayerStat.ssMaxTime;
        ssTimeReload = SuperShotLvl * basePlayerStat.ssTimeReload;

        //Armor Shield
        shipArmor = ArmorShieldLvl * basePlayerStat.shipArmor;
        shipShield = ArmorShieldLvl * basePlayerStat.shipShield;
        shipShieldDelta = ArmorShieldLvl * basePlayerStat.shipShieldDelta;

        //Engine
        shipMaxSpeed = CalculatStat(basePlayerStat.shipMaxSpeed, maxPlayerStat.shipMaxSpeed, EngineLvl); //EngineLvl * basePlayerStat.shipMaxSpeed;
        shipAcceleration = EngineLvl * basePlayerStat.shipAcceleration;
        shipRotation = EngineLvl * basePlayerStat.shipRotation;

        //Super boost
        sbMaxSpeed = SuperBoostLvl * basePlayerStat.sbMaxSpeed;
        sbAcceleration = SuperBoostLvl * basePlayerStat.sbAcceleration;
        sbMaxTime = SuperBoostLvl * basePlayerStat.sbMaxTime;
        sbTimeReload = SuperBoostLvl * basePlayerStat.sbTimeReload;
    }
    private Save CreateSaveGameObject()
    {
        //создаем экземпляр класса Save т заполняем все его поля
        Save save = new Save();

        save.money = money;
        save.levelComplite = levelComplite;

        save.CannonLvl = CannonLvl;
        save.SuperShotLvl = SuperShotLvl;
        save.ArmorShieldLvl = ArmorShieldLvl;
        save.EngineLvl = EngineLvl;
        save.SuperBoostLvl = SuperBoostLvl;
        //save = PlayerStat;
        //возврощаем экземпляр класса
        return save;
    }

    public void SaveGame()
    {
        //В экземпляр класса сохроняем класс с забитыми полями
        Save save = CreateSaveGameObject();
        //создаем бинарную переменную для последующей сериализации
        BinaryFormatter bf = new BinaryFormatter();
        //открываем файл куда будем сохранять
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        //сериализуем класс в файл
        bf.Serialize(file, save);
        //закрываем класс
        file.Close();
        RefreshStat();
        

        //Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            //PlayerStat = save;

            
            money = save.money;
            levelComplite = save.levelComplite;

            CannonLvl = save.CannonLvl;
            SuperShotLvl = save.SuperShotLvl;
            ArmorShieldLvl = save.ArmorShieldLvl;
            EngineLvl = save.EngineLvl;
            SuperBoostLvl = save.SuperBoostLvl;

            /*
            canonDamage = save.canonDamage;
            canonFireRate = save.canonFireRate;
            canonSpeed = save.canonSpeed;
            canonCount = save.canonCount;


            ssDamage = save.ssDamage;
            ssFireRate = save.ssFireRate;
            ssMaxTime = save.ssMaxTime;
            ssTimeReload = save.ssTimeReload;


            shipArmor = save.shipArmor;

            shipShield = save.shipShield;
            shipShieldDelta = save.shipShieldDelta;


            shipMaxSpeed = save.shipMaxSpeed;
            shipAcceleration = save.shipAcceleration;
            shipRotation = save.shipRotation;


            sbMaxSpeed = save.sbMaxSpeed;
            sbAcceleration = save.sbAcceleration;
            sbMaxTime = save.sbMaxTime;
            sbTimeReload = save.sbTimeReload;
            */
            RefreshStat();

            //Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
