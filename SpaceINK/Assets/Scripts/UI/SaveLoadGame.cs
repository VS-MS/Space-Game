using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadGame : MonoBehaviour
{

    public void SaveGame()
    {
        DataSave.instance.SaveGame();
    }

    public void LoadGame()
    {
        DataSave.instance.LoadGame();
    }
}
