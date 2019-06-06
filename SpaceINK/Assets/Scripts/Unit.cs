using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum State
    {
        Idle,       //состояние покоя
        Atack,      //цель найдена, атакуем
        Patrol,     //патрулирование
    }

    public float armorPoints = 100;							 //Уровень хп брони корабля
    protected float maxArmorPoints = 100;
	public float shieldPoints = 100;						 //Уровень щита корабля
    protected float maxShieldPoints = 100;

    //счетчик для востановления щита
    public float shieldDelta = 8f;
    protected float shieldTime = 8f;

    public float maxSpeed = 10f;                     // максимальная скорость, которую может развить корабль
    public float boostForce = 400f;                  // ускорение корабля
    public float rotationSpeed = 1f;				 //Скорость вращения корабля 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Получаем урон
    public void ReceiveDamage(float damage)			//простая функция для получения урона(если далее буду вводить shield, нужно переделать!)
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
        if (armorPoints <= 0) Destroy(gameObject, 0.01f);
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
