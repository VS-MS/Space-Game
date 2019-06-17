using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusSlider : MonoBehaviour
{
    //Расположение статусбара по Y координате
    public float upDis = 1f;
    [SerializeField]
    private GameObject sliderArmor;
    [SerializeField]
    private GameObject sliderShield;
    //Ссылка на экземпляр коробля противника, за которым будет следовать статус бар
    public EnemyShip enemyShip;
    //Максимальное значение брони и щита коробля
    public float maxArmor = 100;
    public float maxShield = 100;


    private void Awake()
    {

    }
    public void SetScaleX(float _x)
    {
        this.transform.localScale = new Vector3(_x, 1, 1);
    }
    void Update()
    {
        if (enemyShip == null || enemyShip.shipState == Unit.State.Die)
        {
            Destroy(gameObject);
        }
        else
        {
            sliderArmor.transform.localScale = new Vector3(enemyShip.armorPoints / maxArmor, 1, 1);
            sliderShield.transform.localScale = new Vector3(enemyShip.shieldPoints / maxShield, 1, 1);
            transform.position = enemyShip.transform.position + transform.up * upDis;
        }
        
    }

}
