using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{

    public GameObject target;

    private GameObject playerShip;
    private GameObject controlCanvas;

    private List<GameObject> enemyArray;

    private void Start()
    {
        //запускаем корутину чтобы добавить на сцену корабль игрока
        StartCoroutine(StartGame());
        //находим на сцене игрока и скрываем его
        playerShip = GameObject.FindGameObjectWithTag("Player");
        playerShip.SetActive(false);
        //находим на сцене канвас управления и скрываем его
        controlCanvas = FindObjectOfType<Canvas>().gameObject;
        controlCanvas.SetActive(false);
        SetEnemyArray();
    }

    private void SetEnemyArray()
    {
        //enemyArray = GameObject.FindGameObjectsWithTag("EnemyShip");
        enemyArray = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemyShip"));
        foreach (GameObject enemy in enemyArray)
        {
            //enemyTarget.Add(Instantiate(arrow, enemy.transform.position, enemy.transform.rotation));
            GameObject target_ = Instantiate(target);
            target_.GetComponent<TargetPosition>().target = enemy;
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
