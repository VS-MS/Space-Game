using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameManager : MonoBehaviour
{

    public GameObject target;
    public GameObject arrow;
    private GameObject arrow_;
    private GameObject arrowTarget;

    private GameObject playerShip;
    private GameObject controlCanvas;

    //Тут храним координаты точки, где появился корабль игрока
    private Vector3 startPositionPlayer;

    //Эфект врат лоя старта
    public GameObject startParticle;
    //Префаб врат телепорта
    public GameObject endGate;

    private bool allEnemyDesroy;
    private List<GameObject> enemyArray;


    private void Awake()
    {
        Time.timeScale = 1;
        arrow_ = Instantiate(arrow);
        arrow_.SetActive(false);
    }
    private void Start()
    {
        allEnemyDesroy = false;
        //запускаем корутину чтобы добавить на сцену корабль игрока
        StartCoroutine(StartGame());
        //находим на сцене игрока и скрываем его
        playerShip = GameObject.FindGameObjectWithTag("Player");
        startPositionPlayer = playerShip.transform.position;
        //Создаем эффект врат на старте
        Instantiate(startParticle, startPositionPlayer, new Quaternion(0,0,0,0));
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
        if(enemyArray.Count == 0 & !allEnemyDesroy)
        {
            //запускаем врата для выхоа из уровня и вешаем на них маркер
            CreateEndGate();
            allEnemyDesroy = true;
        }
        if(arrow_.activeSelf)
        {
            arrow_.transform.rotation = Quaternion.Slerp(arrow_.transform.rotation, arrowTarget.GetComponent<TargetPosition>().targetArrow.transform.rotation, 10 * Time.deltaTime);
            arrow_.transform.position = Vector3.MoveTowards(arrow_.transform.position, arrowTarget.GetComponent<TargetPosition>().targetArrow.transform.position, 40 * Time.deltaTime);

            var heading = arrow_.transform.position - arrowTarget.GetComponent<TargetPosition>().targetArrow.transform.position;
            var distance = heading.magnitude;

            if (distance <= 1)
            {
                arrow_.SetActive(false);
            }
        }
    }
    public void CreateEndGate()
    {
        
        GameObject endGate_ =  Instantiate(endGate, startPositionPlayer, new Quaternion(0, 0, 0, 0));
        arrowTarget = Instantiate(target);
        arrowTarget.GetComponent<TargetPosition>().target = endGate_;
        //arrowTarget = arrowTarget.GetComponent<TargetPosition>().targetArrow;
        arrow_.transform.position = playerShip.transform.position;
        arrow_.SetActive(true);
    }
}
