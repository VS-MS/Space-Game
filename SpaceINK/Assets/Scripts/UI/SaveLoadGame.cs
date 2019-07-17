using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    //ссылка на обект типа DataSave для сохранения прогресса
    private DataSave dataSave;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveGame()
    {
        dataSave = FindObjectOfType<DataSave>();
        dataSave.SaveGame();
    }

    public void LoadGame()
    {
        dataSave = FindObjectOfType<DataSave>();
        dataSave.LoadGame();
    }
}
