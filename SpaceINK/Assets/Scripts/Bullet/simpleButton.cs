using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleButton : MonoBehaviour
{

    [SerializeField]
    private float speed = 20.0f; //Скорость пули
    [SerializeField]
    private float delay = 1.1f; //Задержка до самоуничтожения

    private GameObject parent; //тут будем хранить ссылку на объект, который запустил эту пулю
    public GameObject Parent { set { parent = value; } }

    private Vector3 direction; //напрвление запуска пули
    public Vector3 Direction { set { direction = value; } }

    [SerializeField]
    private float damage = 1.0f;//урон
    public float Damage { set { damage = value; } }
    private SpriteRenderer sprite;

    private int maxKillCount = 1;//сколько объектов пуля может прошить на сквозь
    public int MaxKillCount { set { maxKillCount = value; } }

    private int killCount = 0;//счетчик для количества пройденных объектов на сквозь

    public Color color
    {
        set { sprite.color = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
