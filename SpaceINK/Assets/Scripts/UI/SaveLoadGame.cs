using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SaveGame()
    {
        DataSave.instance.SaveGame();
    }

    public void LoadGame()
    {
        DataSave.instance.LoadGame();
    }
}
