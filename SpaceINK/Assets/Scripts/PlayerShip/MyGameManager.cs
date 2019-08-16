using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    public GameObject target;


    private GameObject playerShip;
    private GameObject controlCanvas;

    //private GameObject[] enemyArray;
    private List<GameObject> enemyArray;

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
        //enemyArray = GameObject.FindGameObjectsWithTag("EnemyShip");
        enemyArray = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyShip"));
        foreach (GameObject enemy in enemyArray)
        {
            Debug.Log(enemy);
        }

        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        playerShip.SetActive(true);
        controlCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        enemyArray.RemoveAll(item => item == null);
        if(enemyArray.Count == 0)
        {
            //запускаем врата для выхоа из уровня
        }
    }
}
