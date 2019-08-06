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

    public long money; 
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
            Destroy(this.gameObject);
        }
        LoadGame();//Так делать не стоит, но пока нет законченного меню, придется оставить.
    }

    public void Update()
    {

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
        cannonFireRate = CalculatStat(basePlayerStat.cannonFireRate, maxPlayerStat.cannonFireRate, CannonLvl); //CannonLvl * basePlayerStat.cannonFireRate;
        cannonBulletSpeed = CalculatStat(basePlayerStat.cannonBulletSpeed, maxPlayerStat.cannonBulletSpeed, CannonLvl); //CannonLvl * basePlayerStat.cannonBulletSpeed;
        //тут нужна своя функция, переделаю, если понадобиться пластичность у этого параметра.
        if(CannonLvl <= 5 )
        {
            cannonCount = 1;
            //1
        }
        else
            if (CannonLvl > 5 && CannonLvl <= 10)
            {
                cannonCount = 2;
                //2
            }
            else
                if (CannonLvl > 10 && CannonLvl <= 15)
                {
                    cannonCount = 3;
                    //3
                }
                else
                    if(CannonLvl > 15 && CannonLvl <= 20)
                    {
                        cannonCount = 5;
                        //5
                    }

        

        //Super Shot
        ssDamage = CalculatStat(basePlayerStat.ssDamage, maxPlayerStat.ssDamage, SuperShotLvl); 
        ssMaxTime = CalculatStat(basePlayerStat.ssMaxTime, maxPlayerStat.ssMaxTime, SuperShotLvl); 
        ssTimeReload = CalculatStat(basePlayerStat.ssTimeReload, maxPlayerStat.ssTimeReload, SuperShotLvl); 

        //Armor Shield
        shipArmor = CalculatStat(basePlayerStat.shipArmor, maxPlayerStat.shipArmor, ArmorShieldLvl); 
        shipShield = CalculatStat(basePlayerStat.shipShield, maxPlayerStat.shipShield, ArmorShieldLvl); 
        shipShieldDelta = CalculatStat(basePlayerStat.shipShieldDelta, maxPlayerStat.shipShieldDelta, ArmorShieldLvl); 

        //Engine
        shipMaxSpeed = CalculatStat(basePlayerStat.shipMaxSpeed, maxPlayerStat.shipMaxSpeed, EngineLvl);
        shipAcceleration = CalculatStat(basePlayerStat.shipAcceleration, maxPlayerStat.shipAcceleration, EngineLvl); 
        shipRotation = CalculatStat(basePlayerStat.shipRotation, maxPlayerStat.shipRotation, EngineLvl);

        //Super boost
        sbMaxSpeed = CalculatStat(basePlayerStat.sbMaxSpeed, maxPlayerStat.sbMaxSpeed, SuperBoostLvl);
        sbAcceleration = CalculatStat(basePlayerStat.sbAcceleration, maxPlayerStat.sbAcceleration, SuperBoostLvl); 
        sbMaxTime = CalculatStat(basePlayerStat.sbMaxTime, maxPlayerStat.sbMaxTime, SuperBoostLvl); 
        sbTimeReload = CalculatStat(basePlayerStat.sbTimeReload, maxPlayerStat.sbTimeReload, SuperBoostLvl);
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
        //Проверяем, существует файл с сохранением в директории или.
        //PS Вроде метод File.Exists может вернуть false, даже если файл существует, но на него не хватает прав.
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
            SaveGame();
            Debug.Log("Create new save game");
        }
    }

}
