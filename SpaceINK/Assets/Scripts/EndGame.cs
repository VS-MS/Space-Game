﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private GameObject mainCanvas;
    private GameObject controlPanel;
    private GameObject endLavelPanel;

    private void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider.tag);
        if(collider.tag == "Player")
        {
            DataSave.instance.SaveGame();

            string s = SceneManager.GetActiveScene().name;
            int lvlNumber;
            try
            {
                lvlNumber = Convert.ToInt32(s);
                if(lvlNumber > DataSave.instance.levelComplite)
                {
                    DataSave.instance.levelComplite = lvlNumber;
                    DataSave.instance.SaveGame();
                }
                
            }
            catch (System.FormatException)
            {
                Debug.LogError("Уровень не может быть засчитан. Не верное название сцены, сцена должна называться только целочисленным числом.");
                lvlNumber = 1;
            }
            AdManager.instance.adCounter++;
            Invoke("LoadEndGame", 1f);
        }
    }
          
    private void LoadEndGame()
    {
        mainCanvas = FindObjectOfType<Canvas>().gameObject;
        if (mainCanvas)
        {
            controlPanel = mainCanvas.transform.Find("PanelControl").gameObject;
            endLavelPanel = mainCanvas.transform.Find("LevelComplite").gameObject;
        }

        controlPanel.SetActive(false);
        Time.timeScale = 0;
        endLavelPanel.GetComponent<MoneyEndGame>().UpdateMoneyEnd();
        endLavelPanel.SetActive(true);
    }
}
