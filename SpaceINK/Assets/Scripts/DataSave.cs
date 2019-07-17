using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public int money;
    public float time_;
    private int counter;
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
    }

    private Save CreateSaveGameObject()
    {
        //создаем экземпляр класса Save т заполняем все его поля
        Save save = new Save();
        save.time = time_;
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

            time_ = save.time;

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
