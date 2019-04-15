using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int armorPoints;							 //Уровень хп брони корабля
	public int shieldPoint;							 //Уровень щита корабля

    public float maxSpeed = 10f;                     // максимальная скорость, которую может развить корабль
    public float boostForce = 400f;                  // ускорение корабля
    public float rotationSpeed = 1f;				 //Скорость вращения корабля 

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void ReciveDamage(int damage)			//простая функция для получения урона(если далее буду вводить shield, нужно переделать!)
	{
		armorPoints -= damage; 
	}
    // Update is called once per frame
    void Update()
    {
        
    }
}
