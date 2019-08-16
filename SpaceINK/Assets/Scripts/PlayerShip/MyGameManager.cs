using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    private GameObject playerShip;
    private GameObject controlCanvas;

    //Счетчик времени для обновления списка противников на карте
    private float deltaTime = 10;
    private float timeTmp;

    private GameObject[] enemyArray;

    private void Start()
    {
        StartCoroutine(StartGame());
        playerShip = GameObject.FindGameObjectWithTag("Player");
        playerShip.SetActive(false);
        controlCanvas = FindObjectOfType<Canvas>().gameObject;
        controlCanvas.SetActive(false);
        EnemyArray();
    }

    private void EnemyArray()
    {
        enemyArray = GameObject.FindGameObjectsWithTag("EnemyShip");
        foreach(GameObject enemy in enemyArray)
        {
            Debug.Log(enemy);
        }

        Debug.Log("///////////////////////////////");
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        playerShip.SetActive(true);
        controlCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {/*
        timeTmp += Time.deltaTime;
        if(timeTmp >= deltaTime)
        {
            timeTmp = 0;
            EnemyArray();
        }
        */
    }
}
