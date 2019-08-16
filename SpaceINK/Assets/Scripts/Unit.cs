using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    //количество очков, которые получит игрок за уничтожение коробля
    [SerializeField]
    protected int moneyCount;
    public int moneySet
    {
        set
        {
            if (value < 0)
                moneyCount = 0;
            else moneyCount = value;
        }
    }

    public State shipState = State.Idle; //переменная, в которой храним состояние корабля
    public enum State
    {
        Idle,       //состояние покоя
        Attack,      //цель найдена, атакуем 
        Patrol,     //патрулирование
        Die,        //состояние для отключения всех скриптов и включения эффекта взрыва
        SuperBoost,      //состояние ускорение, для включения анимации и отключения орудий
        Shoot,
        SuperShoot,
    }
    //тут храним экземпляр VFX взрыва()
    public GameObject[] particleBoom;
    //флаг(костыль) для смерти
    protected bool flagDie = false;

    [Header("Health Settings")]
    //Уровень хп брони корабля
    public float armorPoints = 100;				 
    protected float maxArmorPoints = 100;
    //Уровень щита корабля
    public float shieldPoints = 100;						 
    protected float maxShieldPoints = 100;

    //счетчик для востановления щита
    public float shieldDelta = 8f;
    protected float shieldTime = 8f;

    [Header("Speed Settings")]
    public float maxSpeed = 10f;                     // максимальная скорость, которую может развить корабль
    public float boostForce = 400f;                  // ускорение корабля
    public float rotationSpeed = 1f;				 //Скорость вращения корабля 

    //Получаем урон
    public void ReceiveDamage(float damage)			//простая функция для получения урона
	{
        if (shieldPoints < damage)
        {
            damage -= shieldPoints;
            shieldPoints = 0;
            armorPoints -= damage;
        }
        else
        {
            shieldPoints -= damage;
        }
        //armorPoints -= damage;
        if (armorPoints <= 0 && shipState != State.Die)
        {
            shipState = State.Die;
            //создаем систему частиц для взрыва
            if(particleBoom.Length > 0)
            {
                Instantiate(particleBoom[Random.Range(0, particleBoom.Length)], transform.position, transform.rotation);
            }
            

            Destroy(gameObject, 10.01f);
            if(this.tag == "EnemyShip")
            {
                if (moneyCount > 0)
                {
                    //ищем на сцене объект, в котором храним прогресс игрока и добовляем денег за уничтоженный корабль.
                    DataSave.instance.money += moneyCount;
                    moneyCount = 0;
                }
            }
        }
    }

    //Уничтожаем объект с задержкой
    public virtual void Die(float delay)
    {
        Destroy(gameObject, delay);
    }

    //Проигрыватель для звуков
    public void PlayAudioAtPoint(AudioClip clip, float pitch, float soundvolume, float delayed)
    {
        GameObject go = new GameObject("Audio Shot");
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = soundvolume;
        source.pitch = pitch;
        source.PlayDelayed(delayed);
        GameObject.Destroy(go, clip.length);
        //Woooooof !!!
    }

}
