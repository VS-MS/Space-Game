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
        mainCanvas = FindObjectOfType<Canvas>().gameObject;
        if(mainCanvas)
        {
            controlPanel = mainCanvas.transform.Find("PanelControl").gameObject;
            endLavelPanel = mainCanvas.transform.Find("LevelComplite").gameObject;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            controlPanel.SetActive(false);
            Time.timeScale = 0;
            endLavelPanel.SetActive(true);
        }
    }
            
}
