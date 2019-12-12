using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LastLvl : MonoBehaviour
{
    public int maxLvl;
    private Button nextLvl;
    private void Awake()
    {
        string s = SceneManager.GetActiveScene().name;
        int lvlNumber;
        try
        {
            lvlNumber = Convert.ToInt32(s);
        }
        catch (System.FormatException)
        {
            lvlNumber = 1;
            Debug.LogError("Не верное название сцены, сцена должна называться только целочисленным числом. Установленно значение по умолчанию равное = " + lvlNumber);
        }
        Debug.Log(lvlNumber);
        Debug.Log(maxLvl);

        nextLvl = GetComponent<Button>();
        if(lvlNumber >= maxLvl)
        {
            nextLvl.interactable = false;
        }
        else
        {
            nextLvl.interactable = true;
        }

    }
}
