using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class DataSave : MonoBehaviour
{
    public float time_;
    private int counter;
    static bool created = false;
    void Awake()
    {
        LoadGame();
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        counter++;
        if (counter == 500) SaveGame();
    }

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.time = time_;

        return save;
    }

    public void SaveGame()
    {
        // 1
        Save save = CreateSaveGameObject();

        // 2
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();

        // 3
        //money_ = 0;

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // 1
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            // 2
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            time_ = save.time;
            Debug.Log("Load money = " + time_);

            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }

}
