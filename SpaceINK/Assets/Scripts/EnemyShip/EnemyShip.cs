using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Unit
{
    //Радиус обнаружения противника
    public float radarRadius = 30;
    //Точка, по которой будет стрелять турель в случае обнаружения цели, по умолчанию это сама цель
    public Vector3 targetingPosition;
    

    protected enum State
    {
        Idle,       //состояние покоя
        Atack,      //цель найдена, атакуем
        Patrol,     //патрулирование
    }
    //начальное и текущее состояние
    protected State state = State.Idle;
    protected GameObject playerShip;
    //вектор выходящий из данного объекта в корабль игрока
    protected Vector3 heading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Для удобства - в окне редактора покажем радиус поражения турели и некоторые дополнительные данные
    
}
