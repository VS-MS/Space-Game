using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public int money;
    public int levelComplite;

    [Header("Canon")]
    public float canonDamage;
    public float canonFireRate;
    public float canonSpeed;
    public int canonCount;

    [Header("Super Shot")]
    public float ssDamage;
    public float ssFireRate;
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

    static bool created = false;
    void Awake()
    {
        LoadGame();//Так делать не стоит, но пока нет законченного меню, придется оставить.
        //Проверяем, есть ли экземпляр объекта на сцене, если есть, удаляем этот экземпляр
        if (!created)
        {
            //DontDestroyOnLoad не удоляет объект при загрузки другой сцены.
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private Save CreateSaveGameObject()
    {
        //создаем экземпляр класса Save т заполняем все его поля
        Save save = new Save
        {
            money = money,
            levelComplite = levelComplite,


            canonDamage = canonDamage,
            canonFireRate = canonFireRate,
            canonSpeed = canonSpeed,
            canonCount = canonCount,


            ssDamage = ssDamage,
            ssFireRate = ssFireRate,
            ssMaxTime = ssMaxTime,
            ssTimeReload = ssTimeReload,


            shipArmor = shipArmor,

            shipShield = shipShield,
            shipShieldDelta = shipShieldDelta,


            shipMaxSpeed = shipMaxSpeed,
            shipAcceleration = shipAcceleration,
            shipRotation = shipRotation,


            sbMaxSpeed = sbMaxSpeed,
            sbAcceleration = sbAcceleration,
            sbMaxTime = sbMaxTime,
            sbTimeReload = sbTimeReload
        };
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

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            money = save.money;
            levelComplite = save.levelComplite;


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
            
            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
