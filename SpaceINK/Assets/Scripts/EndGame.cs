using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        endLavelPanel.SetActive(true);
    }
}
